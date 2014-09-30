namespace FunScript.HTML

open FunScript
open FunScript.TypeScript

[<ReflectedDefinition>]
module Helpers =
    open System.Text.RegularExpressions
                
    [<JSEmitInline("{0}[{1}]")>]
    let private getProperty (x: obj) (key: string): string = failwith "never"

    /// Inflater for templates with moustache syntax
    let getInflater (templateId: string) =
        let r = Regex(@"{{\s*(\w+|.)\s*}}")
        let tmp = Globals.document.createElement("div")
        let template = Globals.document.getElementById(templateId).innerHTML.Trim()
        fun (dataSource: obj) ->
            tmp.innerHTML <- r.Replace(template, fun m ->
                let key = m.Groups.[1].Value
                if key = "." then string dataSource else getProperty dataSource key)
            tmp.firstElementChild

    let stripHTML =
        let tmp = Globals.document.createElement_div()
        fun str ->
            tmp.innerHTML <- str
            if tmp.textContent <> null then tmp.textContent
            elif tmp.innerText <> null then tmp.innerText
            else ""

    let rec lookUpward (el: HTMLElement) (soughtTag: string) (stopTag: string) =
        if el.tagName.ToUpper() = soughtTag.ToUpper()
        then Some(el)
        elif el.tagName.ToUpper() = stopTag.ToUpper()
        then None
        else lookUpward el.parentElement soughtTag stopTag
