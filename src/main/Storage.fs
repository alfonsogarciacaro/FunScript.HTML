namespace FunScript.HTML

[<ReflectedDefinition; AutoOpen>]
module Storage =
    open FunScript.TypeScript

    type DBStoreAgs = {
        keyPath: string
        autoIncrement: bool
    }
    type DBStoreKey =
        | None
        | Path of string
        | AutoIncrement
        | PathOrAutoIncrement of string

    type IDBImplementation =
        abstract member Version: uint32
        abstract member Upgrade: IDBDatabase -> unit

    type ReadOnlyStore<'T> =
        member internal ts.original with get() = unbox<IDBObjectStore> ts

        member ts.getAsync(key: obj) =
            Async.FromContinuations(fun (cont, econt, _) ->
                let request = ts.original.get(key)
                request.onerror <- fun _ -> box(econt(exn request.error.name))
                request.onsuccess <- fun _ -> box(cont(unbox<'T> request.result))
            )

    type ReadWriteStore<'T> =
        inherit ReadOnlyStore<'T>

        /// Insert only method
        member ts.add(item: 'T, ?key: obj) =
            ignore(ts.original.add(item, key))

        member ts.addAsync(item: 'T, ?key: obj) =
            Async.FromContinuations(fun (cont, econt, _) ->
                let request = ts.original.add(item, key)
                request.onerror <- fun _ -> box(econt(exn request.error.name))
                request.onsuccess <- fun _ -> box(cont(unbox<'T> request.result))
            )

        member ts.deleteAsync(key: obj) =
            Async.FromContinuations(fun (cont, econt, _) ->
                let request = ts.original.delete(key)
                request.onerror <- fun _ -> box(econt(exn request.error.name))
                request.onsuccess <- fun _ -> box(cont())
            )
        
        /// Delete all items in the store
        member ts.clearAsync() =
            Async.FromContinuations(fun (cont, econt, _) ->
                let request = ts.original.clear()
                request.onerror <- fun _ -> box(econt(exn request.error.name))
                request.onsuccess <- fun _ -> box(cont())
            )


    type TypedIndexedDb<'T when 'T :> IDBImplementation and 'T : (new: unit->'T)>() =
        member private x.useAsync (mkTransaction: IDBDatabase->IDBTransaction) (execTransaction: IDBTransaction->Async<'Result>) =
            Async.FromContinuations(fun (cont, econt, _) ->
                let impl = new 'T()
                let name = typeof<'T>.Name
                let request = Globals.indexedDB._open(name, float impl.Version)
                request.onerror <- fun _ -> 
                    box(econt(exn request.error.name))
                request.onupgradeneeded <- fun ev -> 
                    let db = unbox<IDBDatabase> request.result
                    box(impl.Upgrade(db))
                request.onsuccess <- fun _ ->
                    Async.StartImmediate(async {
                        let db = unbox<IDBDatabase> request.result
                        let trans = mkTransaction db
                        let! res = execTransaction trans
                        trans.oncomplete <- fun _ -> db.close(); box(cont(res))
                        trans.onerror <- fun _ -> db.close(); box(econt(exn trans.error.name))
                    })
                    null
            )
        [<CompiledName("UseStoreReadOnly1")>]
        member x.useStore<'S1,'Result>(transaction: ReadOnlyStore<'S1>->Async<'Result>) =
            let storeName1 = typeof<'S1>.Name
            let mkTransaction = fun (db: IDBDatabase) ->
                db.transaction(storeName1, "readonly")
            let execTransaction = fun (trans: IDBTransaction) -> async {
                let store1 = unbox<ReadOnlyStore<'S1>>(trans.objectStore(storeName1))
                return! transaction store1
            }
            x.useAsync mkTransaction execTransaction
        [<CompiledName("UseStoreReadOnly2")>]
        member x.useStore<'S1,'S2,'Result>(transaction: ReadOnlyStore<'S1>->ReadOnlyStore<'S2>->Async<'Result>) =
            let storeName1 = typeof<'S1>.Name
            let storeName2 = typeof<'S2>.Name
            let mkTransaction = fun (db: IDBDatabase) ->
                db.transaction([|storeName1;storeName2|], "readonly")
            let execTransaction = fun (trans: IDBTransaction) -> async {
                let store1 = unbox<ReadOnlyStore<'S1>>(trans.objectStore(storeName1))
                let store2 = unbox<ReadOnlyStore<'S2>>(trans.objectStore(storeName2))
                return! transaction store1 store2
            }
            x.useAsync mkTransaction execTransaction
        [<CompiledName("UseStoreReadOnly3")>]
        member x.useStore<'S1,'S2,'S3,'Result>(transaction: ReadOnlyStore<'S1>->ReadOnlyStore<'S2>->ReadOnlyStore<'S3>->Async<'Result>) =
            let storeName1 = typeof<'S1>.Name
            let storeName2 = typeof<'S2>.Name
            let storeName3 = typeof<'S3>.Name
            let mkTransaction = fun (db: IDBDatabase) ->
                db.transaction([|storeName1;storeName2;storeName3|], "readonly")
            let execTransaction = fun (trans: IDBTransaction) -> async {
                let store1 = unbox<ReadOnlyStore<'S1>>(trans.objectStore(storeName1))
                let store2 = unbox<ReadOnlyStore<'S2>>(trans.objectStore(storeName2))
                let store3 = unbox<ReadOnlyStore<'S3>>(trans.objectStore(storeName3))
                return! transaction store1 store2 store3
            }
            x.useAsync mkTransaction execTransaction
        [<CompiledName("UseStoreReadOnly4")>]
        member x.useStore<'S1,'S2,'S3,'S4,'Result>(transaction: ReadOnlyStore<'S1>->ReadOnlyStore<'S2>->ReadOnlyStore<'S3>->ReadOnlyStore<'S4>->Async<'Result>) =
            let storeName1 = typeof<'S1>.Name
            let storeName2 = typeof<'S2>.Name
            let storeName3 = typeof<'S3>.Name
            let storeName4 = typeof<'S4>.Name
            let mkTransaction = fun (db: IDBDatabase) ->
                db.transaction([|storeName1;storeName2;storeName3;storeName4|], "readonly")
            let execTransaction = fun (trans: IDBTransaction) -> async {
                let store1 = unbox<ReadOnlyStore<'S1>>(trans.objectStore(storeName1))
                let store2 = unbox<ReadOnlyStore<'S2>>(trans.objectStore(storeName2))
                let store3 = unbox<ReadOnlyStore<'S3>>(trans.objectStore(storeName3))
                let store4 = unbox<ReadOnlyStore<'S4>>(trans.objectStore(storeName4))
                return! transaction store1 store2 store3 store4
            }
            x.useAsync mkTransaction execTransaction

        [<CompiledName("UseStoreReadWrite1")>]
        member x.useStoreRW(transaction: ReadWriteStore<'S1>->Async<'Result>) =
            let storeName1 = typeof<'S1>.Name
            let mkTransaction = fun (db: IDBDatabase) ->
                db.transaction(storeName1, "readwrite")
            let execTransaction = fun (trans: IDBTransaction) -> async {
                let store1 = unbox<ReadWriteStore<'S1>>(trans.objectStore(storeName1))
                return! transaction store1
            }
            x.useAsync mkTransaction execTransaction

        [<CompiledName("UseStoreReadWrite2")>]
        member x.useStoreRW(transaction: ReadWriteStore<'S1>->ReadWriteStore<'S2>->Async<'Result>) =
            let storeName1 = typeof<'S1>.Name
            let storeName2 = typeof<'S2>.Name
            let mkTransaction = fun (db: IDBDatabase) ->
                db.transaction([|storeName1; storeName2|], "readwrite")
            let execTransaction = fun (trans: IDBTransaction) -> async {
                let store1 = unbox<ReadWriteStore<'S1>>(trans.objectStore(storeName1))
                let store2 = unbox<ReadWriteStore<'S2>>(trans.objectStore(storeName2))
                return! transaction store1 store2
            }
            x.useAsync mkTransaction execTransaction

        [<CompiledName("UseStoreReadWrite3")>]
        member x.useStoreRW<'S1,'S2,'S3,'Result>(transaction: ReadWriteStore<'S1>->ReadWriteStore<'S2>->ReadWriteStore<'S3>->Async<'Result>) =
            let storeName1 = typeof<'S1>.Name
            let storeName2 = typeof<'S2>.Name
            let storeName3 = typeof<'S3>.Name
            let mkTransaction = fun (db: IDBDatabase) ->
                db.transaction([|storeName1;storeName2;storeName3|], "readwrite")
            let execTransaction = fun (trans: IDBTransaction) -> async {
                let store1 = unbox<ReadWriteStore<'S1>>(trans.objectStore(storeName1))
                let store2 = unbox<ReadWriteStore<'S2>>(trans.objectStore(storeName2))
                let store3 = unbox<ReadWriteStore<'S3>>(trans.objectStore(storeName3))
                return! transaction store1 store2 store3
            }
            x.useAsync mkTransaction execTransaction
        [<CompiledName("UseStoreReadWrite4")>]
        member x.useStoreRW<'S1,'S2,'S3,'S4,'Result>(transaction: ReadWriteStore<'S1>->ReadWriteStore<'S2>->ReadWriteStore<'S3>->ReadWriteStore<'S4>->Async<'Result>) =
            let storeName1 = typeof<'S1>.Name
            let storeName2 = typeof<'S2>.Name
            let storeName3 = typeof<'S3>.Name
            let storeName4 = typeof<'S4>.Name
            let mkTransaction = fun (db: IDBDatabase) ->
                db.transaction([|storeName1;storeName2;storeName3;storeName4|], "readwrite")
            let execTransaction = fun (trans: IDBTransaction) -> async {
                let store1 = unbox<ReadWriteStore<'S1>>(trans.objectStore(storeName1))
                let store2 = unbox<ReadWriteStore<'S2>>(trans.objectStore(storeName2))
                let store3 = unbox<ReadWriteStore<'S3>>(trans.objectStore(storeName3))
                let store4 = unbox<ReadWriteStore<'S4>>(trans.objectStore(storeName4))
                return! transaction store1 store2 store3 store4
            }
            x.useAsync mkTransaction execTransaction