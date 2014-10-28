module FunScript.HTML.Components
open FunScript
open Microsoft.FSharp.Quotations

let private toPropertyNameFromLambda =
    CompilerComponent.create <| fun (|Split|) compiler returnStrategy ->
        function
        | Patterns.Quote(Patterns.Lambda(_,Patterns.PropertyGet(_, pi, _))) ->
            compiler.Compile returnStrategy (Expr.Value(pi.Name))
        | _ -> []

let getHTMLComponents() =
     [
        [
            toPropertyNameFromLambda
            ExpressionReplacer.createUnsafe <@ Microsoft.FSharp.Control.Observable.take @> <@ ObservableExtensions.take @>
            ExpressionReplacer.createUnsafe <@ Microsoft.FSharp.Control.Observable.skip @> <@ ObservableExtensions.skip @>
            ExpressionReplacer.createUnsafe <@ Microsoft.FSharp.Control.Observable.takeWhile @> <@ ObservableExtensions.takeWhile @>
            ExpressionReplacer.createUnsafe <@ Microsoft.FSharp.Control.Observable.skipWhile @> <@ ObservableExtensions.skipWhile @>

//            ExpressionReplacer.createUnsafe <@ fun (c: System.Drawing.Color) -> new System.Drawing.Pen(c) @> <@ Drawing.Pen.FromColor @> 
//            ExpressionReplacer.createUnsafe <@ fun (c: System.Drawing.Color, w) -> new System.Drawing.Pen(c, w) @> <@ Drawing.Pen.FromColorAndWith @> 
        ]

        ExpressionReplacer.createTypeMethodMappings
            typeof<System.Drawing.Graphics>
            typeof<Drawing.Graphics>
        ExpressionReplacer.createTypeMethodMappings
            typeof<System.Drawing.Color>
            typeof<Drawing.Color>
        ExpressionReplacer.createTypeMethodMappings
            typeof<System.Drawing.Pen>
            typeof<Drawing.Pen>
        ExpressionReplacer.createTypeMethodMappings
            typeof<System.Drawing.Pens>
            typeof<Drawing.Pens>

     ] |> List.concat
