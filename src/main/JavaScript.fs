namespace FunScript.HTML

open System
open FunScript
open FunScript.TypeScript

module JS =
    /// When this module is opened, the ? operator will be overloaded to support dynamic properties like C#
    module Dynamic =
        [<JSEmitInline("({0}[{1}])")>]
        let (?) (ob: obj) (prop: string): 'a = failwith "never"

        [<JSEmitInline("({0}[{1}] = {2})")>]
        let (?<-) (ob: obj) (prop: string) (value: 'a): unit = failwith "never"

    // TODO: Write a simple enumerator for objects?
    // TODO: Include 'instanceof'?

    [<JSEmitInline("(typeof {0})")>]
    let typeof (o: obj): string = failwith "never"

    [<JSEmitInline("({0}*1.0)")>]
    let number (a:obj) : float = failwith "never"

    [<JSEmitInline("this")>]
    let this() = failwith "never"

    [<JSEmitInline("arguments")>]
    let arguments(): obj[] = failwith "never"

    [<JSEmitInline("({0} === undefined)")>]
    let isUndefined (o: obj): bool = failwith "never"

    [<JSEmitInline("({0} !== undefined)")>]
    let notUndefined (o: obj): bool = failwith "never"

    [<FunScript.JSEmitInline("({0} || {1})")>]
    let fallback (value: obj) (fallbackValue: obj): 'a = failwith "never"

    /// Use this to pipeline fallbacks
    [<FunScript.JSEmitInline("({1} || {0})")>]
    let fallbackRev (fallbackValue: obj) (value: obj): 'a = failwith "never"

    [<JSEmit("{2}[{0}] = {1}; return {2};")>]
    let add (key: string) (value: obj) (``object``: obj): obj = failwith "never"

    [<JSEmitInline("{1}[{0}]")>]
    let get<'a> (key: string) (``object``: obj): 'a = failwith "never"

    [<JSEmitInline("{2}[{0}] = {1}")>]
    let set (key: string) (value: obj) (``object``: obj): unit = failwith "never"
