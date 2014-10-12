namespace FunScript.HTML

[<ReflectedDefinition; AutoOpen>]
module Storage =
    open FunScript.TypeScript
    open Microsoft.FSharp.Quotations

    type DBKeyMethod<'T,'TKey> =
        | KeyPath of Expr<'T->'TKey>
        | AutoIncrement

    type DBCursorDirection =
        /// This direction causes the cursor to be opened at the start of the source.
        /// When iterated, the cursor should yield all records, including duplicates, in monotonically increasing order of keys.
        | Next
        /// This direction causes the cursor to be opened at the start of the source.
        /// When iterated, the cursor should not yield records with the same key, but otherwise yield all records, in monotonically increasing order of keys.
        /// For every key with duplicate values, only the first record is yielded.
        /// When the source is an object store or a unique index, this direction has the exact same behavior as Next.
        | NextUnique
        /// This direction causes the cursor to be opened at the end of the source. 
        /// When iterated, the cursor should yield all records, including duplicates, in monotonically decreasing order of keys.
        | Prev
        /// This direction causes the cursor to be opened at the end of the source.
        /// When iterated, the cursor should not yield records with the same key, but otherwise yield all records, in monotonically decreasing order of keys.
        /// For every key with duplicate values, only the first record is yielded. 
        /// When the source is an object store or a unique index, this direction has the exact same behavior as Prev.
        | PrevUnique
        static member Default with get() = Next
        member dir.toW3Convention() =
            match dir with
            | Next -> "next"
            | NextUnique -> "nextunique"
            | Prev -> "prev"
            | PrevUnique -> "prevunique"

    type DBStoreUpgrade<'T> =
        /// Unique will default to false
        member x.createIndex<'TIndex>(expr: Expr<'T->'TIndex>, ?unique: bool) =
            let indexName: string = unbox expr
            let unique = defaultArg unique false
            let args = System.Collections.Generic.Dictionary<_,_>()
            args.Add("unique", unique)
            ignore((unbox x: IDBObjectStore).createIndex(indexName, indexName, args))

        member x.deleteIndex<'TIndex>(expr: Expr<'T->'TIndex>) =
            (unbox x: IDBObjectStore).deleteIndex(unbox expr)

    type DBUpgrade =
        member db.createStore<'T,'TKey>(keyMethod: DBKeyMethod<'T,'TKey>) =
            let args = System.Collections.Generic.Dictionary<_,_>()
            match keyMethod with
            | KeyPath pathExpr -> args.Add("keyPath", box pathExpr)
            | AutoIncrement -> args.Add("autoIncrement", box true)
            let storeName = typeof<'T>.Name
            unbox<DBStoreUpgrade<'T>>((unbox db: IDBDatabase).createObjectStore(storeName, args))

        member db.deleteStore<'T>() =
            let storeName = typeof<'T>.Name
            (unbox db: IDBDatabase).deleteObjectStore(storeName)

    type DBImplementation =
        abstract member Version: uint32
        abstract member Upgrade: DBUpgrade -> unit

    type internal DBSeqCursor<'T> =
        static member openCursor(index: IDBIndex, keyCursor: bool, range: IDBKeyRange option, direction: DBCursorDirection option, step: uint32 option) =
            let range = defaultArg range Unchecked.defaultof<IDBKeyRange>
            let direction = (defaultArg direction DBCursorDirection.Default).toW3Convention()
            let step = defaultArg step 1u
            let request =
                match keyCursor with
                | false -> index.openCursor(range, direction)
                | true -> index.openKeyCursor(range, direction)
            let rec cursorSeq() = asyncSeq {
                let! result = Async.FromContinuations(fun (cont, _, _) ->
                    request.onsuccess <- fun _ ->
                        request.onsuccess <- null
                        if unbox<bool> request.result then
                            let cursor = unbox<IDBCursorWithValue> request.result
                            cont(Some(unbox<'T> cursor.value))
                            cursor.advance(float step)
                        else
                            cont(None)
                        null)
                match result with
                | None -> ()
                | Some r ->
                    yield r
                    yield! cursorSeq()
            }
            cursorSeq()

    type DBIndexAsync<'T,'TIndex> =
        member internal x.original with get() = unbox<IDBIndex> x

        member x.countAllAsync() =
            Async.FromContinuations(fun (cont, econt, _) ->
                let request = x.original.count()
                request.onerror <- fun _ -> box(econt(exn request.error.name))
                request.onsuccess <- fun _ -> box(cont(unbox<int> request.result))
            )

        /// The parameter should not target the object primary key, but the property used by the index.
        member x.countKeyAsync(indexKey: 'TIndex) =
            Async.FromContinuations(fun (cont, econt, _) ->
                let request = x.original.count(indexKey)
                request.onerror <- fun _ -> box(econt(exn request.error.name))
                request.onsuccess <- fun _ -> box(cont(unbox<int> request.result))
            )

        /// The parameter should not target the object primary key, but the property used by the index.
        member x.countRangeAsync(indexKeyRange: IDBKeyRange) =
            Async.FromContinuations(fun (cont, econt, _) ->
                let request = x.original.count(indexKeyRange)
                request.onerror <- fun _ -> box(econt(exn request.error.name))
                request.onsuccess <- fun _ -> box(cont(unbox<int> request.result))
            )

        /// The parameter should not target the object primary key, but the property used by the index.
        member x.getAsync(indexKey: 'TIndex) =
            Async.FromContinuations(fun (cont, econt, _) ->
                let request = x.original.get(indexKey)
                request.onerror <- fun _ -> box(econt(exn request.error.name))
                request.onsuccess <- fun _ -> box(cont(unbox<'T> request.result))
            )

        /// The parameter should not target the object primary key, but the property used by the index.
        member x.getFirstAsync(indexKeyRange: IDBKeyRange) =
            Async.FromContinuations(fun (cont, econt, _) ->
                let request = x.original.get(indexKeyRange)
                request.onerror <- fun _ -> box(econt(exn request.error.name))
                request.onsuccess <- fun _ -> box(cont(unbox<'T> request.result))
            )

        /// The parameter should not target the object primary key, but the property used by the index.
        /// The return value is the object actual primary key.
        member x.getKeyAsync(indexKey: obj) =
            Async.FromContinuations(fun (cont, econt, _) ->
                let request = x.original.getKey(indexKey)
                request.onerror <- fun _ -> box(econt(exn request.error.name))
                request.onsuccess <- fun _ -> box(cont(request.result))
            )

        /// The parameter should not target the object primary key, but the property used by the index.
        /// The return value is the object actual primary key.
        member x.getKeyFirstAsync(indexKeyRange: IDBKeyRange) =
            Async.FromContinuations(fun (cont, econt, _) ->
                let request = x.original.getKey(indexKeyRange)
                request.onerror <- fun _ -> box(econt(exn request.error.name))
                request.onsuccess <- fun _ -> box(cont(request.result))
            )

        /// If no range is passed, then the range includes all records.
        /// If no direction is passed, it will default to "next".
        member x.openCursor(?range: IDBKeyRange, ?direction: DBCursorDirection, ?step: uint32) =
            DBSeqCursor<'T>.openCursor(unbox x, keyCursor=false, range=range, direction=direction, step=step)

        /// If no range is passed, then the range includes all keys.
        /// If no direction is passed, it will default to "next".
        member x.openKeyCursor(?range: IDBKeyRange, ?direction: DBCursorDirection, ?step: uint32) =
            DBSeqCursor<obj>.openCursor(unbox x, keyCursor=true, range=range, direction=direction, step=step)

    type DBStore<'T> =
        member internal x.original with get() = unbox<IDBObjectStore> x

        member x.getAsync(key: obj) =
            Async.FromContinuations(fun (cont, econt, _) ->
                let request = x.original.get(key)
                request.onerror <- fun _ -> box(econt(exn request.error.name))
                request.onsuccess <- fun _ -> box(cont(unbox<'T> request.result))
            )

        /// If no range is passed, it will default to a key range that selects all the records in this object store.
        /// If no direction is passed, it will default to "next".
        member x.openCursor(?range: IDBKeyRange, ?direction: DBCursorDirection, ?step: uint32) =
            DBSeqCursor<'T>.openCursor(unbox x, keyCursor=false, range=range, direction=direction, step=step)

        member x.index(expr: Expr<'T->'TIndex>) =
            unbox<DBIndexAsync<'T,'TIndex>>(x.original.index(unbox expr))

        member x.indexUnsafe(expr: Expr) =
            unbox<DBIndexAsync<'T,obj>>(x.original.index(unbox expr))

    type DBStoreRW<'T> =
        inherit DBStore<'T>

        /// Insert only method. The operation is asynchronous but request is being ignored.
        member x.add(item: 'T) =
            ignore(x.original.add(item))

        /// Insert only method. It can be used in async workflows. The return value is key set for the stored record.
        /// Note the continuation is called when the operation is added to the queue, but you still need to wait for the transaction to complete.
        member x.addAsync(item: 'T) =
            Async.FromContinuations(fun (cont, econt, _) ->
                let request = x.original.add(item)
                request.onerror <- fun _ -> box(econt(exn request.error.name))
                request.onsuccess <- fun _ -> box(cont(request.result))
            )

        /// Delete the item specified by the key. The operation is asynchronous but request is being ignored.
        member x.delete(key: obj) =
            ignore(x.original.delete(key))

        /// Delete the item specified by the key. It can be used in async workflows.
        /// Note the continuation is called when the operation is added to the queue, but you still need to wait for the transaction to complete.
        member x.deleteAsync(key: obj) =
            Async.FromContinuations(fun (cont, econt, _) ->
                let request = x.original.delete(key)
                request.onerror <- fun _ -> box(econt(exn request.error.name))
                request.onsuccess <- fun _ -> box(cont())
            )

        /// Delete all items in the store. The operation is asynchronous but request is being ignored.
        member x.clear() =
            ignore(x.original.clear())
        
        /// Delete all items in the store. It can be used in async workflows.
        /// Note the continuation is called when the operation is added to the queue, but you still need to wait for the transaction to complete.
        member x.clearAsync() =
            Async.FromContinuations(fun (cont, econt, _) ->
                let request = x.original.clear()
                request.onerror <- fun _ -> box(econt(exn request.error.name))
                request.onsuccess <- fun _ -> box(cont())
            )

         /// Update or Insert method. The operation is asynchronous but request is being ignored.
        member x.put(item: 'T) =
            ignore(x.original.put(item))

        /// Update or insert method. It can be used in async workflows. The return value is key set for the stored record.
        /// Note the continuation is called when the operation is added to the queue, but you still need to wait for the transaction to complete.
        member x.putAsync(item: 'T) =
            Async.FromContinuations(fun (cont, econt, _) ->
                let request = x.original.put(item)
                request.onerror <- fun _ -> box(econt(exn request.error.name))
                request.onsuccess <- fun _ -> box(cont(request.result))
            )

    type IndexedDB<'T when 'T :> DBImplementation and 'T : (new: unit->'T)>() =
        member x.deleteDatabaseAsync() =
            Async.FromContinuations(fun (cont, econt, _) ->
                let name = typeof<'T>.Name
                let request = Globals.indexedDB.deleteDatabase(name)
                request.onerror <- fun _ -> box(econt(exn request.error.name))
                request.onsuccess <- fun _ -> box(cont())
            )
        member private x.useAsync (mkTransaction: IDBDatabase->IDBTransaction) (execTransaction: IDBTransaction->Async<'Result>) =
            Async.FromContinuations(fun (cont, econt, _) ->
                let impl = new 'T() :> DBImplementation
                let name = typeof<'T>.Name
                let request = Globals.indexedDB._open(name, float impl.Version)
                request.onerror <- fun _ -> 
                    box(econt(exn request.error.name))
                request.onupgradeneeded <- fun ev ->
                    box(try
                            let db = unbox<IDBDatabase> request.result
                            impl.Upgrade(unbox db)
                        with
                            | e -> econt(e))
                request.onsuccess <- fun _ ->
                    Async.StartImmediate(async {
                        try
                            let db = unbox<IDBDatabase> request.result
                            let trans = mkTransaction db
                            let! res = execTransaction trans
                            trans.oncomplete <- fun _ -> db.close(); box(cont(res))
                            trans.onerror <- fun _ -> db.close(); box(econt(exn trans.error.name))
                        with
                        | e -> econt(e)
                    })
                    null
            )
        [<CompiledName("UseStoreReadOnly1")>]
        member x.useStore<'S1,'Result>(transaction: DBStore<'S1>->Async<'Result>) =
            let storeName1 = typeof<'S1>.Name
            let mkTransaction = fun (db: IDBDatabase) ->
                db.transaction(storeName1, "readonly")
            let execTransaction = fun (trans: IDBTransaction) -> async {
                let store1 = unbox<DBStore<'S1>>(trans.objectStore(storeName1))
                return! transaction store1
            }
            x.useAsync mkTransaction execTransaction
        [<CompiledName("UseStoreReadOnly2")>]
        member x.useStore<'S1,'S2,'Result>(transaction: DBStore<'S1>->DBStore<'S2>->Async<'Result>) =
            let storeName1 = typeof<'S1>.Name
            let storeName2 = typeof<'S2>.Name
            let mkTransaction = fun (db: IDBDatabase) ->
                db.transaction([|storeName1;storeName2|], "readonly")
            let execTransaction = fun (trans: IDBTransaction) -> async {
                let store1 = unbox<DBStore<'S1>>(trans.objectStore(storeName1))
                let store2 = unbox<DBStore<'S2>>(trans.objectStore(storeName2))
                return! transaction store1 store2
            }
            x.useAsync mkTransaction execTransaction
        [<CompiledName("UseStoreReadOnly3")>]
        member x.useStore<'S1,'S2,'S3,'Result>(transaction: DBStore<'S1>->DBStore<'S2>->DBStore<'S3>->Async<'Result>) =
            let storeName1 = typeof<'S1>.Name
            let storeName2 = typeof<'S2>.Name
            let storeName3 = typeof<'S3>.Name
            let mkTransaction = fun (db: IDBDatabase) ->
                db.transaction([|storeName1;storeName2;storeName3|], "readonly")
            let execTransaction = fun (trans: IDBTransaction) -> async {
                let store1 = unbox<DBStore<'S1>>(trans.objectStore(storeName1))
                let store2 = unbox<DBStore<'S2>>(trans.objectStore(storeName2))
                let store3 = unbox<DBStore<'S3>>(trans.objectStore(storeName3))
                return! transaction store1 store2 store3
            }
            x.useAsync mkTransaction execTransaction
        [<CompiledName("UseStoreReadOnly4")>]
        member x.useStore<'S1,'S2,'S3,'S4,'Result>(transaction: DBStore<'S1>->DBStore<'S2>->DBStore<'S3>->DBStore<'S4>->Async<'Result>) =
            let storeName1 = typeof<'S1>.Name
            let storeName2 = typeof<'S2>.Name
            let storeName3 = typeof<'S3>.Name
            let storeName4 = typeof<'S4>.Name
            let mkTransaction = fun (db: IDBDatabase) ->
                db.transaction([|storeName1;storeName2;storeName3;storeName4|], "readonly")
            let execTransaction = fun (trans: IDBTransaction) -> async {
                let store1 = unbox<DBStore<'S1>>(trans.objectStore(storeName1))
                let store2 = unbox<DBStore<'S2>>(trans.objectStore(storeName2))
                let store3 = unbox<DBStore<'S3>>(trans.objectStore(storeName3))
                let store4 = unbox<DBStore<'S4>>(trans.objectStore(storeName4))
                return! transaction store1 store2 store3 store4
            }
            x.useAsync mkTransaction execTransaction

        [<CompiledName("UseStoreReadWrite1")>]
        member x.useStoreRW<'S1,'Result>(transaction: DBStoreRW<'S1>->Async<'Result>) =
            let storeName1 = typeof<'S1>.Name
            let mkTransaction = fun (db: IDBDatabase) ->
                db.transaction(storeName1, "readwrite")
            let execTransaction = fun (trans: IDBTransaction) -> async {
                let store1 = unbox<DBStoreRW<'S1>>(trans.objectStore(storeName1))
                return! transaction store1
            }
            x.useAsync mkTransaction execTransaction

        [<CompiledName("UseStoreReadWrite2")>]
        member x.useStoreRW<'S1,'S2,'Result>(transaction: DBStoreRW<'S1>->DBStoreRW<'S2>->Async<'Result>) =
            let storeName1 = typeof<'S1>.Name
            let storeName2 = typeof<'S2>.Name
            let mkTransaction = fun (db: IDBDatabase) ->
                db.transaction([|storeName1; storeName2|], "readwrite")
            let execTransaction = fun (trans: IDBTransaction) -> async {
                let store1 = unbox<DBStoreRW<'S1>>(trans.objectStore(storeName1))
                let store2 = unbox<DBStoreRW<'S2>>(trans.objectStore(storeName2))
                return! transaction store1 store2
            }
            x.useAsync mkTransaction execTransaction

        [<CompiledName("UseStoreReadWrite3")>]
        member x.useStoreRW<'S1,'S2,'S3,'Result>(transaction: DBStoreRW<'S1>->DBStoreRW<'S2>->DBStoreRW<'S3>->Async<'Result>) =
            let storeName1 = typeof<'S1>.Name
            let storeName2 = typeof<'S2>.Name
            let storeName3 = typeof<'S3>.Name
            let mkTransaction = fun (db: IDBDatabase) ->
                db.transaction([|storeName1;storeName2;storeName3|], "readwrite")
            let execTransaction = fun (trans: IDBTransaction) -> async {
                let store1 = unbox<DBStoreRW<'S1>>(trans.objectStore(storeName1))
                let store2 = unbox<DBStoreRW<'S2>>(trans.objectStore(storeName2))
                let store3 = unbox<DBStoreRW<'S3>>(trans.objectStore(storeName3))
                return! transaction store1 store2 store3
            }
            x.useAsync mkTransaction execTransaction
        [<CompiledName("UseStoreReadWrite4")>]
        member x.useStoreRW<'S1,'S2,'S3,'S4,'Result>(transaction: DBStoreRW<'S1>->DBStoreRW<'S2>->DBStoreRW<'S3>->DBStoreRW<'S4>->Async<'Result>) =
            let storeName1 = typeof<'S1>.Name
            let storeName2 = typeof<'S2>.Name
            let storeName3 = typeof<'S3>.Name
            let storeName4 = typeof<'S4>.Name
            let mkTransaction = fun (db: IDBDatabase) ->
                db.transaction([|storeName1;storeName2;storeName3;storeName4|], "readwrite")
            let execTransaction = fun (trans: IDBTransaction) -> async {
                let store1 = unbox<DBStoreRW<'S1>>(trans.objectStore(storeName1))
                let store2 = unbox<DBStoreRW<'S2>>(trans.objectStore(storeName2))
                let store3 = unbox<DBStoreRW<'S3>>(trans.objectStore(storeName3))
                let store4 = unbox<DBStoreRW<'S4>>(trans.objectStore(storeName4))
                return! transaction store1 store2 store3 store4
            }
            x.useAsync mkTransaction execTransaction