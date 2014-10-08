namespace FunScript.HTML
open FunScript.TypeScript

[<AutoOpen; ReflectedDefinition>]
module DrawingExtensions =
    type System.Drawing.Graphics with
        static member FromCanvas(canvasId: string) =
            let canvas = Globals.document.getElementById(canvasId) |> unbox<HTMLCanvasElement>
            canvas.getContext("2d") |> unbox<System.Drawing.Graphics>

[<ReflectedDefinition>]
module Drawing =

    type Point(x: float, y: float) =
        member val X = x with get, set
        member val Y = y with get, set
        static member Create(x: float, y: float) = Point(x, y) 

    // TODO: Implement Size?
    type Rectangle(x: float, y: float, width: float, height: float) =
        member val X = x with get, set
        member val Y = y with get, set
        member val Width = width with get, set
        member val Height = height with get, set
        static member Create(x: float, y: float, width: float, height: float) = Rectangle(x, y, width, height)

    type Graphics private () =
        let mutable pen: Pen = Unchecked.defaultof<Pen>

        member g.context
            with get() =
                unbox<CanvasRenderingContext2D> g

        member g.setPen(value: Pen) =
            if not(obj.ReferenceEquals(value, pen)) then
                pen <- value
                let ctx = g.context
                ctx.strokeStyle <- pen.Color
                ctx.lineWidth <- pen.Width
                ctx.miterLimit <- pen.MiterLimit
                let lineJoint =
                    match pen.LineJoin with
                    | System.Drawing.Drawing2D.LineJoin.Bevel -> "bevel"
                    | System.Drawing.Drawing2D.LineJoin.Miter
                    | System.Drawing.Drawing2D.LineJoin.MiterClipped -> "miter"
                    | System.Drawing.Drawing2D.LineJoin.Round -> "round"
                    | _ -> failwith "lineJoint not recognized"
                ctx.lineJoin <- lineJoint

                if pen.DashStyle = System.Drawing.Drawing2D.DashStyle.Solid
                then
                    // TODO: I just give precedence to EndCap. Is there a better way?
                    let cap = if pen.EndCap <> System.Drawing.Drawing2D.LineCap.Flat then pen.EndCap else pen.StartCap
                    let cap' =
                        match cap with
                        | System.Drawing.Drawing2D.LineCap.Flat -> "butt"
                        | System.Drawing.Drawing2D.LineCap.Round -> "round"
                        | System.Drawing.Drawing2D.LineCap.Square -> "square"
                        | _ -> failwith "lineCap not recognized"
                    ctx.lineCap <- cap'
                    ctx.setLineDash([||])
                else
                    let pattern =
                        match pen.DashStyle with
                        | System.Drawing.Drawing2D.DashStyle.Custom -> pen.DashPattern
                        | System.Drawing.Drawing2D.DashStyle.Dash ->  [|12.; 3.|]
                        | System.Drawing.Drawing2D.DashStyle.DashDot ->  [|12.; 3.; 3.; 3.|]
                        | System.Drawing.Drawing2D.DashStyle.DashDotDot ->  [|12.; 3.; 3.; 3.; 3.; 3.|]
                        | System.Drawing.Drawing2D.DashStyle.Dot ->  [|3.; 3.|]
                        | _ -> failwith "lineDash not recognized"
                    ctx.lineCap <- "butt" // NOTE: Dash lines are not working if this is set otherwise
                    ctx.setLineDash(pattern)
                    ctx.lineDashOffset <- pen.DashOffset

        member g.DrawRectangle(pen: Pen, x: float, y: float, width: float, height: float) =
            g.setPen(pen)
            g.context.strokeRect(x, y, width, height)

        member g.DrawLine(pen: Pen, x1: float, y1: float, x2: float, y2: float) =
            g.setPen(pen)
            let ctx = g.context
            ctx.beginPath()
            ctx.moveTo(x1, y1)
            ctx.lineTo(x2, y2)
            ctx.closePath()

        member g.DrawLines(pen: Pen, points: Point[]) =
            if points.Length > 1 then
                g.setPen(pen)
                let ctx = g.context
                ctx.beginPath()
                ctx.moveTo(points.[0].X, points.[0].Y)
                for i = 1 to points.Length - 1 do
                    let p = points.[i]
                    ctx.lineTo(p.X, p.Y)
                ctx.closePath()

    // TODO: Add Brush, CompoundArray and Transform (Alignment?, PenType?, DashCap?) properties and methods. Implement IDisposable
    and Pen(color: Color, width: float) =
        let mutable width = width
        let mutable color = color
        let mutable miterLimit = 10.
        let mutable dashOffset = 0.
        let mutable dashPattern: float array = [||]
        let mutable dashStyle = System.Drawing.Drawing2D.DashStyle.Solid
        let mutable startCap = System.Drawing.Drawing2D.LineCap.Flat
        let mutable endCap = System.Drawing.Drawing2D.LineCap.Flat
        let mutable lineJoin = System.Drawing.Drawing2D.LineJoin.Miter

        static member FromColorAndWith(color: Color, width: float) = Pen(color, width)
        static member FromColor(color) = Pen(color, 1.)

        member pen.Color with get() = color and set(value) = color <- value
        member pen.Width with get() = width and set(value) = width <- value
        member pen.MiterLimit with get() = miterLimit and set(value) = miterLimit <- value
        member pen.StartCap with get() = startCap and set(value) = startCap <- value
        member pen.EndCap with get() = endCap and set(value) = endCap <- value
        member pen.LineJoin with get() = lineJoin and set(value) = lineJoin <- value

        member pen.DashOffset with get() = dashOffset and set(value) = dashOffset <- value
        member pen.DashStyle with get() = dashStyle and set(value) = dashStyle <- value
        member pen.DashPattern
            with get() =
                dashPattern
            and set(value) =
                dashPattern <- value
                dashStyle <- System.Drawing.Drawing2D.DashStyle.Custom

    and Color() =
        let x = 1

        static member FromName() = failwith "never"
        static member FromArgb() = failwith "never"

        static member AliceBlue with get() = unbox<Color> "#F0F8FF"
        static member AntiqueWhite with get() = unbox<Color> "#FAEBD7"
        static member Aqua with get() = unbox<Color> "#00FFFF"
        static member Aquamarine with get() = unbox<Color> "#7FFFD4"
        static member Azure with get() = unbox<Color> "#F0FFFF"
        static member Beige with get() = unbox<Color> "#F5F5DC"
        static member Bisque with get() = unbox<Color> "#FFE4C4"
        static member Black with get() = unbox<Color> "#000000"
        static member BlanchedAlmond with get() = unbox<Color> "#FFEBCD"
        static member Blue with get() = unbox<Color> "#0000FF"
        static member BlueViolet with get() = unbox<Color> "#8A2BE2"
        static member Brown with get() = unbox<Color> "#A52A2A"
        static member BurlyWood with get() = unbox<Color> "#DEB887"
        static member CadetBlue with get() = unbox<Color> "#5F9EA0"
        static member Chartreuse with get() = unbox<Color> "#7FFF00"
        static member Chocolate with get() = unbox<Color> "#D2691E"
        static member Coral with get() = unbox<Color> "#FF7F50"
        static member CornflowerBlue with get() = unbox<Color> "#6495ED"
        static member Cornsilk with get() = unbox<Color> "#FFF8DC"
        static member Crimson with get() = unbox<Color> "#DC143C"
        static member Cyan with get() = unbox<Color> "#00FFFF"
        static member DarkBlue with get() = unbox<Color> "#00008B"
        static member DarkCyan with get() = unbox<Color> "#008B8B"
        static member DarkGoldenrod with get() = unbox<Color> "#B8860B"
        static member DarkGray with get() = unbox<Color> "#A9A9A9"
        static member DarkGreen with get() = unbox<Color> "#006400"
        static member DarkKhaki with get() = unbox<Color> "#BDB76B"
        static member DarkMagenta with get() = unbox<Color> "#8B008B"
        static member DarkOliveGreen with get() = unbox<Color> "#556B2F"
        static member DarkOrange with get() = unbox<Color> "#FF8C00"
        static member DarkOrchid with get() = unbox<Color> "#9932CC"
        static member DarkRed with get() = unbox<Color> "#8B0000"
        static member DarkSalmon with get() = unbox<Color> "#E9967A"
        static member DarkSeaGreen with get() = unbox<Color> "#8FBC8F"
        static member DarkSlateBlue with get() = unbox<Color> "#483D8B"
        static member DarkSlateGray with get() = unbox<Color> "#2F4F4F"
        static member DarkTurquoise with get() = unbox<Color> "#00CED1"
        static member DarkViolet with get() = unbox<Color> "#9400D3"
        static member DeepPink with get() = unbox<Color> "#FF1493"
        static member DeepSkyBlue with get() = unbox<Color> "#00BFFF"
        static member DimGray with get() = unbox<Color> "#696969"
        static member DodgerBlue with get() = unbox<Color> "#1E90FF"
        static member Firebrick with get() = unbox<Color> "#B22222"
        static member FloralWhite with get() = unbox<Color> "#FFFAF0"
        static member ForestGreen with get() = unbox<Color> "#228B22"
        static member Fuchsia with get() = unbox<Color> "#FF00FF"
        static member Gainsboro with get() = unbox<Color> "#DCDCDC"
        static member GhostWhite with get() = unbox<Color> "#F8F8FF"
        static member Gold with get() = unbox<Color> "#FFD700"
        static member Goldenrod with get() = unbox<Color> "#DAA520"
        static member Gray with get() = unbox<Color> "#808080"
        static member Green with get() = unbox<Color> "#008000"
        static member GreenYellow with get() = unbox<Color> "#ADFF2F"
        static member Honeydew with get() = unbox<Color> "#F0FFF0"
        static member HotPink with get() = unbox<Color> "#FF69B4"
        static member IndianRed with get() = unbox<Color> "#CD5C5C"
        static member Indigo with get() = unbox<Color> "#4B0082"
        static member Ivory with get() = unbox<Color> "#FFFFF0"
        static member Khaki with get() = unbox<Color> "#F0E68C"
        static member Lavender with get() = unbox<Color> "#E6E6FA"
        static member LavenderBlush with get() = unbox<Color> "#FFF0F5"
        static member LawnGreen with get() = unbox<Color> "#7CFC00"
        static member LemonChiffon with get() = unbox<Color> "#FFFACD"
        static member LightBlue with get() = unbox<Color> "#ADD8E6"
        static member LightCoral with get() = unbox<Color> "#F08080"
        static member LightCyan with get() = unbox<Color> "#E0FFFF"
        static member LightGoldenrodYellow with get() = unbox<Color> "#FAFAD2"
        static member LightGray with get() = unbox<Color> "#D3D3D3"
        static member LightGreen with get() = unbox<Color> "#90EE90"
        static member LightPink with get() = unbox<Color> "#FFB6C1"
        static member LightSalmon with get() = unbox<Color> "#FFA07A"
        static member LightSeaGreen with get() = unbox<Color> "#20B2AA"
        static member LightSkyBlue with get() = unbox<Color> "#87CEFA"
        static member LightSlateGray with get() = unbox<Color> "#778899"
        static member LightSteelBlue with get() = unbox<Color> "#B0C4DE"
        static member LightYellow with get() = unbox<Color> "#FFFFE0"
        static member Lime with get() = unbox<Color> "#00FF00"
        static member LimeGreen with get() = unbox<Color> "#32CD32"
        static member Linen with get() = unbox<Color> "#FAF0E6"
        static member Magenta with get() = unbox<Color> "#FF00FF"
        static member Maroon with get() = unbox<Color> "#800000"
        static member MediumAquamarine with get() = unbox<Color> "#66CDAA"
        static member MediumBlue with get() = unbox<Color> "#0000CD"
        static member MediumOrchid with get() = unbox<Color> "#BA55D3"
        static member MediumPurple with get() = unbox<Color> "#9370DB"
        static member MediumSeaGreen with get() = unbox<Color> "#3CB371"
        static member MediumSlateBlue with get() = unbox<Color> "#7B68EE"
        static member MediumSpringGreen with get() = unbox<Color> "#00FA9A"
        static member MediumTurquoise with get() = unbox<Color> "#48D1CC"
        static member MediumVioletRed with get() = unbox<Color> "#C71585"
        static member MidnightBlue with get() = unbox<Color> "#191970"
        static member MintCream with get() = unbox<Color> "#F5FFFA"
        static member MistyRose with get() = unbox<Color> "#FFE4E1"
        static member Moccasin with get() = unbox<Color> "#FFE4B5"
        static member NavajoWhite with get() = unbox<Color> "#FFDEAD"
        static member Navy with get() = unbox<Color> "#000080"
        static member OldLace with get() = unbox<Color> "#FDF5E6"
        static member Olive with get() = unbox<Color> "#808000"
        static member OliveDrab with get() = unbox<Color> "#6B8E23"
        static member Orange with get() = unbox<Color> "#FFA500"
        static member OrangeRed with get() = unbox<Color> "#FF4500"
        static member Orchid with get() = unbox<Color> "#DA70D6"
        static member PaleGoldenrod with get() = unbox<Color> "#EEE8AA"
        static member PaleGreen with get() = unbox<Color> "#98FB98"
        static member PaleTurquoise with get() = unbox<Color> "#AFEEEE"
        static member PaleVioletRed with get() = unbox<Color> "#DB7093"
        static member PapayaWhip with get() = unbox<Color> "#FFEFD5"
        static member PeachPuff with get() = unbox<Color> "#FFDAB9"
        static member Peru with get() = unbox<Color> "#CD853F"
        static member Pink with get() = unbox<Color> "#FFC0CB"
        static member Plum with get() = unbox<Color> "#DDA0DD"
        static member PowderBlue with get() = unbox<Color> "#B0E0E6"
        static member Purple with get() = unbox<Color> "#800080"
        static member Red with get() = unbox<Color> "#FF0000"
        static member RosyBrown with get() = unbox<Color> "#BC8F8F"
        static member RoyalBlue with get() = unbox<Color> "#4169E1"
        static member SaddleBrown with get() = unbox<Color> "#8B4513"
        static member Salmon with get() = unbox<Color> "#FA8072"
        static member SandyBrown with get() = unbox<Color> "#F4A460"
        static member SeaGreen with get() = unbox<Color> "#2E8B57"
        static member SeaShell with get() = unbox<Color> "#FFF5EE"
        static member Sienna with get() = unbox<Color> "#A0522D"
        static member Silver with get() = unbox<Color> "#C0C0C0"
        static member SkyBlue with get() = unbox<Color> "#87CEEB"
        static member SlateBlue with get() = unbox<Color> "#6A5ACD"
        static member SlateGray with get() = unbox<Color> "#708090"
        static member Snow with get() = unbox<Color> "#FFFAFA"
        static member SpringGreen with get() = unbox<Color> "#00FF7F"
        static member SteelBlue with get() = unbox<Color> "#4682B4"
        static member Tan with get() = unbox<Color> "#D2B48C"
        static member Teal with get() = unbox<Color> "#008080"
        static member Thistle with get() = unbox<Color> "#D8BFD8"
        static member Tomato with get() = unbox<Color> "#FF6347"
        static member Transparent with get() = unbox<Color> "#FFFFFF" // TODO
        static member Turquoise with get() = unbox<Color> "#40E0D0"
        static member Violet with get() = unbox<Color> "#EE82EE"
        static member Wheat with get() = unbox<Color> "#F5DEB3"
        static member White with get() = unbox<Color> "#FFFFFF"
        static member WhiteSmoke with get() = unbox<Color> "#F5F5F5"
        static member Yellow with get() = unbox<Color> "#FFFF00"
        static member YellowGreen with get() = unbox<Color> "#9ACD32"


    type Pens =
        static member AliceBlue with get() = Pen.FromColor(unbox<Color> "#F0F8FF")
        static member AntiqueWhite with get() = Pen.FromColor(unbox<Color> "#FAEBD7")
        static member Aqua with get() = Pen.FromColor(unbox<Color> "#00FFFF")
        static member Aquamarine with get() = Pen.FromColor(unbox<Color> "#7FFFD4")
        static member Azure with get() = Pen.FromColor(unbox<Color> "#F0FFFF")
        static member Beige with get() = Pen.FromColor(unbox<Color> "#F5F5DC")
        static member Bisque with get() = Pen.FromColor(unbox<Color> "#FFE4C4")
        static member Black with get() = Pen.FromColor(unbox<Color> "#000000")
        static member BlanchedAlmond with get() = Pen.FromColor(unbox<Color> "#FFEBCD")
        static member Blue with get() = Pen.FromColor(unbox<Color> "#0000FF")
        static member BlueViolet with get() = Pen.FromColor(unbox<Color> "#8A2BE2")
        static member Brown with get() = Pen.FromColor(unbox<Color> "#A52A2A")
        static member BurlyWood with get() = Pen.FromColor(unbox<Color> "#DEB887")
        static member CadetBlue with get() = Pen.FromColor(unbox<Color> "#5F9EA0")
        static member Chartreuse with get() = Pen.FromColor(unbox<Color> "#7FFF00")
        static member Chocolate with get() = Pen.FromColor(unbox<Color> "#D2691E")
        static member Coral with get() = Pen.FromColor(unbox<Color> "#FF7F50")
        static member CornflowerBlue with get() = Pen.FromColor(unbox<Color> "#6495ED")
        static member Cornsilk with get() = Pen.FromColor(unbox<Color> "#FFF8DC")
        static member Crimson with get() = Pen.FromColor(unbox<Color> "#DC143C")
        static member Cyan with get() = Pen.FromColor(unbox<Color> "#00FFFF")
        static member DarkBlue with get() = Pen.FromColor(unbox<Color> "#00008B")
        static member DarkCyan with get() = Pen.FromColor(unbox<Color> "#008B8B")
        static member DarkGoldenrod with get() = Pen.FromColor(unbox<Color> "#B8860B")
        static member DarkGray with get() = Pen.FromColor(unbox<Color> "#A9A9A9")
        static member DarkGreen with get() = Pen.FromColor(unbox<Color> "#006400")
        static member DarkKhaki with get() = Pen.FromColor(unbox<Color> "#BDB76B")
        static member DarkMagenta with get() = Pen.FromColor(unbox<Color> "#8B008B")
        static member DarkOliveGreen with get() = Pen.FromColor(unbox<Color> "#556B2F")
        static member DarkOrange with get() = Pen.FromColor(unbox<Color> "#FF8C00")
        static member DarkOrchid with get() = Pen.FromColor(unbox<Color> "#9932CC")
        static member DarkRed with get() = Pen.FromColor(unbox<Color> "#8B0000")
        static member DarkSalmon with get() = Pen.FromColor(unbox<Color> "#E9967A")
        static member DarkSeaGreen with get() = Pen.FromColor(unbox<Color> "#8FBC8F")
        static member DarkSlateBlue with get() = Pen.FromColor(unbox<Color> "#483D8B")
        static member DarkSlateGray with get() = Pen.FromColor(unbox<Color> "#2F4F4F")
        static member DarkTurquoise with get() = Pen.FromColor(unbox<Color> "#00CED1")
        static member DarkViolet with get() = Pen.FromColor(unbox<Color> "#9400D3")
        static member DeepPink with get() = Pen.FromColor(unbox<Color> "#FF1493")
        static member DeepSkyBlue with get() = Pen.FromColor(unbox<Color> "#00BFFF")
        static member DimGray with get() = Pen.FromColor(unbox<Color> "#696969")
        static member DodgerBlue with get() = Pen.FromColor(unbox<Color> "#1E90FF")
        static member Firebrick with get() = Pen.FromColor(unbox<Color> "#B22222")
        static member FloralWhite with get() = Pen.FromColor(unbox<Color> "#FFFAF0")
        static member ForestGreen with get() = Pen.FromColor(unbox<Color> "#228B22")
        static member Fuchsia with get() = Pen.FromColor(unbox<Color> "#FF00FF")
        static member Gainsboro with get() = Pen.FromColor(unbox<Color> "#DCDCDC")
        static member GhostWhite with get() = Pen.FromColor(unbox<Color> "#F8F8FF")
        static member Gold with get() = Pen.FromColor(unbox<Color> "#FFD700")
        static member Goldenrod with get() = Pen.FromColor(unbox<Color> "#DAA520")
        static member Gray with get() = Pen.FromColor(unbox<Color> "#808080")
        static member Green with get() = Pen.FromColor(unbox<Color> "#008000")
        static member GreenYellow with get() = Pen.FromColor(unbox<Color> "#ADFF2F")
        static member Honeydew with get() = Pen.FromColor(unbox<Color> "#F0FFF0")
        static member HotPink with get() = Pen.FromColor(unbox<Color> "#FF69B4")
        static member IndianRed with get() = Pen.FromColor(unbox<Color> "#CD5C5C")
        static member Indigo with get() = Pen.FromColor(unbox<Color> "#4B0082")
        static member Ivory with get() = Pen.FromColor(unbox<Color> "#FFFFF0")
        static member Khaki with get() = Pen.FromColor(unbox<Color> "#F0E68C")
        static member Lavender with get() = Pen.FromColor(unbox<Color> "#E6E6FA")
        static member LavenderBlush with get() = Pen.FromColor(unbox<Color> "#FFF0F5")
        static member LawnGreen with get() = Pen.FromColor(unbox<Color> "#7CFC00")
        static member LemonChiffon with get() = Pen.FromColor(unbox<Color> "#FFFACD")
        static member LightBlue with get() = Pen.FromColor(unbox<Color> "#ADD8E6")
        static member LightCoral with get() = Pen.FromColor(unbox<Color> "#F08080")
        static member LightCyan with get() = Pen.FromColor(unbox<Color> "#E0FFFF")
        static member LightGoldenrodYellow with get() = Pen.FromColor(unbox<Color> "#FAFAD2")
        static member LightGray with get() = Pen.FromColor(unbox<Color> "#D3D3D3")
        static member LightGreen with get() = Pen.FromColor(unbox<Color> "#90EE90")
        static member LightPink with get() = Pen.FromColor(unbox<Color> "#FFB6C1")
        static member LightSalmon with get() = Pen.FromColor(unbox<Color> "#FFA07A")
        static member LightSeaGreen with get() = Pen.FromColor(unbox<Color> "#20B2AA")
        static member LightSkyBlue with get() = Pen.FromColor(unbox<Color> "#87CEFA")
        static member LightSlateGray with get() = Pen.FromColor(unbox<Color> "#778899")
        static member LightSteelBlue with get() = Pen.FromColor(unbox<Color> "#B0C4DE")
        static member LightYellow with get() = Pen.FromColor(unbox<Color> "#FFFFE0")
        static member Lime with get() = Pen.FromColor(unbox<Color> "#00FF00")
        static member LimeGreen with get() = Pen.FromColor(unbox<Color> "#32CD32")
        static member Linen with get() = Pen.FromColor(unbox<Color> "#FAF0E6")
        static member Magenta with get() = Pen.FromColor(unbox<Color> "#FF00FF")
        static member Maroon with get() = Pen.FromColor(unbox<Color> "#800000")
        static member MediumAquamarine with get() = Pen.FromColor(unbox<Color> "#66CDAA")
        static member MediumBlue with get() = Pen.FromColor(unbox<Color> "#0000CD")
        static member MediumOrchid with get() = Pen.FromColor(unbox<Color> "#BA55D3")
        static member MediumPurple with get() = Pen.FromColor(unbox<Color> "#9370DB")
        static member MediumSeaGreen with get() = Pen.FromColor(unbox<Color> "#3CB371")
        static member MediumSlateBlue with get() = Pen.FromColor(unbox<Color> "#7B68EE")
        static member MediumSpringGreen with get() = Pen.FromColor(unbox<Color> "#00FA9A")
        static member MediumTurquoise with get() = Pen.FromColor(unbox<Color> "#48D1CC")
        static member MediumVioletRed with get() = Pen.FromColor(unbox<Color> "#C71585")
        static member MidnightBlue with get() = Pen.FromColor(unbox<Color> "#191970")
        static member MintCream with get() = Pen.FromColor(unbox<Color> "#F5FFFA")
        static member MistyRose with get() = Pen.FromColor(unbox<Color> "#FFE4E1")
        static member Moccasin with get() = Pen.FromColor(unbox<Color> "#FFE4B5")
        static member NavajoWhite with get() = Pen.FromColor(unbox<Color> "#FFDEAD")
        static member Navy with get() = Pen.FromColor(unbox<Color> "#000080")
        static member OldLace with get() = Pen.FromColor(unbox<Color> "#FDF5E6")
        static member Olive with get() = Pen.FromColor(unbox<Color> "#808000")
        static member OliveDrab with get() = Pen.FromColor(unbox<Color> "#6B8E23")
        static member Orange with get() = Pen.FromColor(unbox<Color> "#FFA500")
        static member OrangeRed with get() = Pen.FromColor(unbox<Color> "#FF4500")
        static member Orchid with get() = Pen.FromColor(unbox<Color> "#DA70D6")
        static member PaleGoldenrod with get() = Pen.FromColor(unbox<Color> "#EEE8AA")
        static member PaleGreen with get() = Pen.FromColor(unbox<Color> "#98FB98")
        static member PaleTurquoise with get() = Pen.FromColor(unbox<Color> "#AFEEEE")
        static member PaleVioletRed with get() = Pen.FromColor(unbox<Color> "#DB7093")
        static member PapayaWhip with get() = Pen.FromColor(unbox<Color> "#FFEFD5")
        static member PeachPuff with get() = Pen.FromColor(unbox<Color> "#FFDAB9")
        static member Peru with get() = Pen.FromColor(unbox<Color> "#CD853F")
        static member Pink with get() = Pen.FromColor(unbox<Color> "#FFC0CB")
        static member Plum with get() = Pen.FromColor(unbox<Color> "#DDA0DD")
        static member PowderBlue with get() = Pen.FromColor(unbox<Color> "#B0E0E6")
        static member Purple with get() = Pen.FromColor(unbox<Color> "#800080")
        static member Red with get() = Pen.FromColor(unbox<Color> "#FF0000")
        static member RosyBrown with get() = Pen.FromColor(unbox<Color> "#BC8F8F")
        static member RoyalBlue with get() = Pen.FromColor(unbox<Color> "#4169E1")
        static member SaddleBrown with get() = Pen.FromColor(unbox<Color> "#8B4513")
        static member Salmon with get() = Pen.FromColor(unbox<Color> "#FA8072")
        static member SandyBrown with get() = Pen.FromColor(unbox<Color> "#F4A460")
        static member SeaGreen with get() = Pen.FromColor(unbox<Color> "#2E8B57")
        static member SeaShell with get() = Pen.FromColor(unbox<Color> "#FFF5EE")
        static member Sienna with get() = Pen.FromColor(unbox<Color> "#A0522D")
        static member Silver with get() = Pen.FromColor(unbox<Color> "#C0C0C0")
        static member SkyBlue with get() = Pen.FromColor(unbox<Color> "#87CEEB")
        static member SlateBlue with get() = Pen.FromColor(unbox<Color> "#6A5ACD")
        static member SlateGray with get() = Pen.FromColor(unbox<Color> "#708090")
        static member Snow with get() = Pen.FromColor(unbox<Color> "#FFFAFA")
        static member SpringGreen with get() = Pen.FromColor(unbox<Color> "#00FF7F")
        static member SteelBlue with get() = Pen.FromColor(unbox<Color> "#4682B4")
        static member Tan with get() = Pen.FromColor(unbox<Color> "#D2B48C")
        static member Teal with get() = Pen.FromColor(unbox<Color> "#008080")
        static member Thistle with get() = Pen.FromColor(unbox<Color> "#D8BFD8")
        static member Tomato with get() = Pen.FromColor(unbox<Color> "#FF6347")
        static member Transparent with get() = Pen.FromColor(unbox<Color> "#FFFFFF") // TODO
        static member Turquoise with get() = Pen.FromColor(unbox<Color> "#40E0D0")
        static member Violet with get() = Pen.FromColor(unbox<Color> "#EE82EE")
        static member Wheat with get() = Pen.FromColor(unbox<Color> "#F5DEB3")
        static member White with get() = Pen.FromColor(unbox<Color> "#FFFFFF")
        static member WhiteSmoke with get() = Pen.FromColor(unbox<Color> "#F5F5F5")
        static member Yellow with get() = Pen.FromColor(unbox<Color> "#FFFF00")
        static member YellowGreen with get() = Pen.FromColor(unbox<Color> "#9ACD32")


    