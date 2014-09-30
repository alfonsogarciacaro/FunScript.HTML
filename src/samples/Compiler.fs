module FunScript.HTML.Samples.Compiler

// Translate the code above to JS and open the html
open System.IO
open Microsoft.FSharp.Quotations

let path = Directory.GetCurrentDirectory()

let compile (expr: Expr) (name: string) =
    let code = FunScript.Compiler.Compiler.Compile(expr, noReturn=true, components=FunScript.HTML.Components.getHTMLComponents())
    File.WriteAllText(Path.Combine(path, name + ".js"), code)
    System.Diagnostics.Process.Start(Path.Combine(path, name + ".html")) |> ignore

compile <@@ FunScript.HTML.Samples.VanillaJS.main() @@> "vanillaJS"
compile <@@ FunScript.HTML.Samples.Ractive.main() @@> "ractive"
