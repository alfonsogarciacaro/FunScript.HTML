namespace FunScript.HTML
open FunScript.TypeScript

[<ReflectedDefinition>]
module Drawing =
    open System
    open System.Text.RegularExpressions

    // TODO: operators
    type Point(x: float, y: float) =
        member val X = x with get, set
        member val Y = y with get, set

    // TODO: operators
    type Size(width: float, height: float) =
        member val Width = width with get, set
        member val Height = height with get, set

    // TODO: operators
    type Rectangle(x: float, y: float, width: float, height: float) =
        member val X = x with get, set
        member val Y = y with get, set
        member val Width = width with get, set
        member val Height = height with get, set

    type Graphics(canvas: HTMLCanvasElement) =
        let ctx = canvas.getContext_2d()
        let mutable pen = Unchecked.defaultof<Pen>

        member g.setPen(value: Pen) =
            if not(obj.ReferenceEquals(value, pen)) then
                pen <- value
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

        member g.Clear(color: Color) =
            ctx.clearRect(0., 0., canvas.width, canvas.height)
            if color.A < 255 then // TODO: Check the underlying string directly for performance?
                let prev = ctx.fillStyle
                ctx.fillRect(0., 0., canvas.width, canvas.height)
                ctx.fillStyle <- prev

        member g.DrawArc(pen: Pen, rect: Rectangle, startAngle: float, sweepAngle: float) =
            failwith "Not implemented"

//        member g.DrawArc(pen: Pen, rect: RectangleF, startAngle: float, sweepAngle: float) =
//            failwith "Not implemented"

//        member g.DrawArc(pen: Pen, x: int, y: int, width: int, height: int, startAngle: int, sweepAngle: int) =
//            failwith "Not implemented"

        member g.DrawArc(pen: Pen, x: float, y: float, width: float, height: float, startAngle: float, sweepAngle: float) =
            let startAngle = startAngle * Math.PI / 180.
            let sweepAngle = sweepAngle * Math.PI / 180.
            let radius = height / 2.
            g.setPen(pen)
            if width <> height then ctx.save(); ctx.scale(width / height, 1.)
            ctx.beginPath()
            ctx.arc(x + (width / 2.), y + radius, radius, startAngle, sweepAngle)
            ctx.stroke()
            if width <> height then ctx.restore()

        member g.DrawBezier(pen: Pen, x1: float, y1: float, x2: float, y2: float, x3: float, y3: float, x4: float, y4: float) =
            g.setPen(pen)
            ctx.beginPath()
            ctx.moveTo(x1, y1)
            ctx.bezierCurveTo(x2, y2, x3, y3, x4, y4)
            ctx.closePath()

        // TODO: DrawBezier overloads and DrawBeziers

        member g.DrawRectangle(pen: Pen, x: float, y: float, width: float, height: float) =
            g.setPen(pen)
            ctx.strokeRect(x, y, width, height)

        member g.DrawLine(pen: Pen, x1: float, y1: float, x2: float, y2: float) =
            g.setPen(pen)
            ctx.beginPath()
            ctx.moveTo(x1, y1)
            ctx.lineTo(x2, y2)
            ctx.closePath()

        member g.DrawLines(pen: Pen, points: Point[]) =
            if points.Length > 1 then
                g.setPen(pen)
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

        new (color) = Pen(color, 1.)
//        static member FromColorAndWith(color: Color, width: float) = Pen(color, width)
//        static member FromColor(color) = Pen(color, 1.)

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

    and Color =
        /// From http://stackoverflow.com/questions/2353211/hsl-to-rgb-color-conversion
        [<FunScript.JSEmit("""var m = /rgba\((\d+),(\d+),(\d+),([\d.]+)\)/.exec({0})
        var r = m[1] / 255, g = m[2] / 255, b = m[3] / 255;
        var max = Math.max(r, g, b), min = Math.min(r, g, b);
        var h, s, l = (max + min) / 2;
        if (max == min) {
            h = s = 0; // achromatic
        } else {
            var d = max - min;
            s = l > 0.5 ? d / (2 - max - min) : d / (max + min);
            switch(max){
                case r: h = (g - b) / d + (g < b ? 6 : 0); break;
                case g: h = (b - r) / d + 2; break;
                case b: h = (r - g) / d + 4; break;
            }
            h /= 6;
        }
        return [h, s, l]""")>]
        static member private toHslArray(color: Color): float[] = failwith "never"

        [<FunScript.JSEmit("""var m = /rgba\((\d+),(\d+),(\d+),([\d.]+)\)/.exec({0})
        var r = m[1], g = m[2], b = m[3], a = Math.floor(m[4] * 255);
        return [a, r, g, b]""")>]
        static member private toArgbArray(color: Color): int[] = failwith "never"

        static member FromArgb(argb: int) = Color.FromArgb(argb >>> 24, (argb >>> 16) ||| 255, (argb >>> 8) ||| 255, argb ||| 255)
        member color.ToArgb() = let argb = Color.toArgbArray(color) in (argb.[0] <<< 24) ||| (argb.[1] <<< 16) ||| (argb.[2] <<< 8) ||| argb.[3]

        member color.A with get() = Color.toArgbArray(color).[0]
        member color.R with get() = Color.toArgbArray(color).[1]
        member color.G with get() = Color.toArgbArray(color).[2]
        member color.B with get() = Color.toArgbArray(color).[3]

        member color.GetHue()        = Color.toHslArray(color).[0]
        member color.GetSaturation() = Color.toHslArray(color).[1]
        member color.GetBrightness() = Color.toHslArray(color).[2]

        static member FromArgb(red: int, green: int, blue: int) = String.Format("rgba({0},{1},{2},1)", red, green, blue)
        static member FromArgb(alpha: int, red: int, green: int, blue: int) = String.Format("rgba({0},{1},{2},{3})", red, green, blue, (float alpha) / 255.)
        static member FromArgb(alpha: int, baseColor: Color) = Regex.Replace(unbox baseColor, "rgba\((\d+),(\d+),(\d+),([\d.]+)\)", String.Format("rgba($1,$2,$3,{0}", (float alpha) / 255.))

        static member AliceBlue with get() = unbox<Color> "rgba(240,248,255,1)"
        static member AntiqueWhite with get() = unbox<Color> "rgba(250,235,215,1)"
        static member Aqua with get() = unbox<Color> "rgba(0,255,255,1)"
        static member Aquamarine with get() = unbox<Color> "rgba(127,255,212,1)"
        static member Azure with get() = unbox<Color> "rgba(240,255,255,1)"
        static member Beige with get() = unbox<Color> "rgba(245,245,220,1)"
        static member Bisque with get() = unbox<Color> "rgba(255,228,196,1)"
        static member Black with get() = unbox<Color> "rgba(0,0,0,1)"
        static member BlanchedAlmond with get() = unbox<Color> "rgba(255,235,205,1)"
        static member Blue with get() = unbox<Color> "rgba(0,0,255,1)"
        static member BlueViolet with get() = unbox<Color> "rgba(138,43,226,1)"
        static member Brown with get() = unbox<Color> "rgba(165,42,42,1)"
        static member BurlyWood with get() = unbox<Color> "rgba(222,184,135,1)"
        static member CadetBlue with get() = unbox<Color> "rgba(95,158,160,1)"
        static member Chartreuse with get() = unbox<Color> "rgba(127,255,0,1)"
        static member Chocolate with get() = unbox<Color> "rgba(210,105,30,1)"
        static member Coral with get() = unbox<Color> "rgba(255,127,80,1)"
        static member CornflowerBlue with get() = unbox<Color> "rgba(100,149,237,1)"
        static member Cornsilk with get() = unbox<Color> "rgba(255,248,220,1)"
        static member Crimson with get() = unbox<Color> "rgba(220,20,60,1)"
        static member Cyan with get() = unbox<Color> "rgba(0,255,255,1)"
        static member DarkBlue with get() = unbox<Color> "rgba(0,0,139,1)"
        static member DarkCyan with get() = unbox<Color> "rgba(0,139,139,1)"
        static member DarkGoldenrod with get() = unbox<Color> "rgba(184,134,11,1)"
        static member DarkGray with get() = unbox<Color> "rgba(169,169,169,1)"
        static member DarkGreen with get() = unbox<Color> "rgba(0,100,0,1)"
        static member DarkKhaki with get() = unbox<Color> "rgba(189,183,107,1)"
        static member DarkMagenta with get() = unbox<Color> "rgba(139,0,139,1)"
        static member DarkOliveGreen with get() = unbox<Color> "rgba(85,107,47,1)"
        static member DarkOrange with get() = unbox<Color> "rgba(255,140,0,1)"
        static member DarkOrchid with get() = unbox<Color> "rgba(153,50,204,1)"
        static member DarkRed with get() = unbox<Color> "rgba(139,0,0,1)"
        static member DarkSalmon with get() = unbox<Color> "rgba(233,150,122,1)"
        static member DarkSeaGreen with get() = unbox<Color> "rgba(143,188,143,1)"
        static member DarkSlateBlue with get() = unbox<Color> "rgba(72,61,139,1)"
        static member DarkSlateGray with get() = unbox<Color> "rgba(47,79,79,1)"
        static member DarkTurquoise with get() = unbox<Color> "rgba(0,206,209,1)"
        static member DarkViolet with get() = unbox<Color> "rgba(148,0,211,1)"
        static member DeepPink with get() = unbox<Color> "rgba(255,20,147,1)"
        static member DeepSkyBlue with get() = unbox<Color> "rgba(0,191,255,1)"
        static member DimGray with get() = unbox<Color> "rgba(105,105,105,1)"
        static member DodgerBlue with get() = unbox<Color> "rgba(30,144,255,1)"
        static member Firebrick with get() = unbox<Color> "rgba(178,34,34,1)"
        static member FloralWhite with get() = unbox<Color> "rgba(255,250,240,1)"
        static member ForestGreen with get() = unbox<Color> "rgba(34,139,34,1)"
        static member Fuchsia with get() = unbox<Color> "rgba(255,0,255,1)"
        static member Gainsboro with get() = unbox<Color> "rgba(220,220,220,1)"
        static member GhostWhite with get() = unbox<Color> "rgba(248,248,255,1)"
        static member Gold with get() = unbox<Color> "rgba(255,215,0,1)"
        static member Goldenrod with get() = unbox<Color> "rgba(218,165,32,1)"
        static member Gray with get() = unbox<Color> "rgba(128,128,128,1)"
        static member Green with get() = unbox<Color> "rgba(0,128,0,1)"
        static member GreenYellow with get() = unbox<Color> "rgba(173,255,47,1)"
        static member Honeydew with get() = unbox<Color> "rgba(240,255,240,1)"
        static member HotPink with get() = unbox<Color> "rgba(255,105,180,1)"
        static member IndianRed with get() = unbox<Color> "rgba(205,92,92,1)"
        static member Indigo with get() = unbox<Color> "rgba(75,0,130,1)"
        static member Ivory with get() = unbox<Color> "rgba(255,255,240,1)"
        static member Khaki with get() = unbox<Color> "rgba(240,230,140,1)"
        static member Lavender with get() = unbox<Color> "rgba(230,230,250,1)"
        static member LavenderBlush with get() = unbox<Color> "rgba(255,240,245,1)"
        static member LawnGreen with get() = unbox<Color> "rgba(124,252,0,1)"
        static member LemonChiffon with get() = unbox<Color> "rgba(255,250,205,1)"
        static member LightBlue with get() = unbox<Color> "rgba(173,216,230,1)"
        static member LightCoral with get() = unbox<Color> "rgba(240,128,128,1)"
        static member LightCyan with get() = unbox<Color> "rgba(224,255,255,1)"
        static member LightGoldenrodYellow with get() = unbox<Color> "rgba(250,250,210,1)"
        static member LightGray with get() = unbox<Color> "rgba(211,211,211,1)"
        static member LightGreen with get() = unbox<Color> "rgba(144,238,144,1)"
        static member LightPink with get() = unbox<Color> "rgba(255,182,193,1)"
        static member LightSalmon with get() = unbox<Color> "rgba(255,160,122,1)"
        static member LightSeaGreen with get() = unbox<Color> "rgba(32,178,170,1)"
        static member LightSkyBlue with get() = unbox<Color> "rgba(135,206,250,1)"
        static member LightSlateGray with get() = unbox<Color> "rgba(119,136,153,1)"
        static member LightSteelBlue with get() = unbox<Color> "rgba(176,196,222,1)"
        static member LightYellow with get() = unbox<Color> "rgba(255,255,224,1)"
        static member Lime with get() = unbox<Color> "rgba(0,255,0,1)"
        static member LimeGreen with get() = unbox<Color> "rgba(50,205,50,1)"
        static member Linen with get() = unbox<Color> "rgba(250,240,230,1)"
        static member Magenta with get() = unbox<Color> "rgba(255,0,255,1)"
        static member Maroon with get() = unbox<Color> "rgba(128,0,0,1)"
        static member MediumAquamarine with get() = unbox<Color> "rgba(102,205,170,1)"
        static member MediumBlue with get() = unbox<Color> "rgba(0,0,205,1)"
        static member MediumOrchid with get() = unbox<Color> "rgba(186,85,211,1)"
        static member MediumPurple with get() = unbox<Color> "rgba(147,112,219,1)"
        static member MediumSeaGreen with get() = unbox<Color> "rgba(60,179,113,1)"
        static member MediumSlateBlue with get() = unbox<Color> "rgba(123,104,238,1)"
        static member MediumSpringGreen with get() = unbox<Color> "rgba(0,250,154,1)"
        static member MediumTurquoise with get() = unbox<Color> "rgba(72,209,204,1)"
        static member MediumVioletRed with get() = unbox<Color> "rgba(199,21,133,1)"
        static member MidnightBlue with get() = unbox<Color> "rgba(25,25,112,1)"
        static member MintCream with get() = unbox<Color> "rgba(245,255,250,1)"
        static member MistyRose with get() = unbox<Color> "rgba(255,228,225,1)"
        static member Moccasin with get() = unbox<Color> "rgba(255,228,181,1)"
        static member NavajoWhite with get() = unbox<Color> "rgba(255,222,173,1)"
        static member Navy with get() = unbox<Color> "rgba(0,0,128,1)"
        static member OldLace with get() = unbox<Color> "rgba(253,245,230,1)"
        static member Olive with get() = unbox<Color> "rgba(128,128,0,1)"
        static member OliveDrab with get() = unbox<Color> "rgba(107,142,35,1)"
        static member Orange with get() = unbox<Color> "rgba(255,165,0,1)"
        static member OrangeRed with get() = unbox<Color> "rgba(255,69,0,1)"
        static member Orchid with get() = unbox<Color> "rgba(218,112,214,1)"
        static member PaleGoldenrod with get() = unbox<Color> "rgba(238,232,170,1)"
        static member PaleGreen with get() = unbox<Color> "rgba(152,251,152,1)"
        static member PaleTurquoise with get() = unbox<Color> "rgba(175,238,238,1)"
        static member PaleVioletRed with get() = unbox<Color> "rgba(219,112,147,1)"
        static member PapayaWhip with get() = unbox<Color> "rgba(255,239,213,1)"
        static member PeachPuff with get() = unbox<Color> "rgba(255,218,185,1)"
        static member Peru with get() = unbox<Color> "rgba(205,133,63,1)"
        static member Pink with get() = unbox<Color> "rgba(255,192,203,1)"
        static member Plum with get() = unbox<Color> "rgba(221,160,221,1)"
        static member PowderBlue with get() = unbox<Color> "rgba(176,224,230,1)"
        static member Purple with get() = unbox<Color> "rgba(128,0,128,1)"
        static member Red with get() = unbox<Color> "rgba(255,0,0,1)"
        static member RosyBrown with get() = unbox<Color> "rgba(188,143,143,1)"
        static member RoyalBlue with get() = unbox<Color> "rgba(65,105,225,1)"
        static member SaddleBrown with get() = unbox<Color> "rgba(139,69,19,1)"
        static member Salmon with get() = unbox<Color> "rgba(250,128,114,1)"
        static member SandyBrown with get() = unbox<Color> "rgba(244,164,96,1)"
        static member SeaGreen with get() = unbox<Color> "rgba(46,139,87,1)"
        static member SeaShell with get() = unbox<Color> "rgba(255,245,238,1)"
        static member Sienna with get() = unbox<Color> "rgba(160,82,45,1)"
        static member Silver with get() = unbox<Color> "rgba(192,192,192,1)"
        static member SkyBlue with get() = unbox<Color> "rgba(135,206,235,1)"
        static member SlateBlue with get() = unbox<Color> "rgba(106,90,205,1)"
        static member SlateGray with get() = unbox<Color> "rgba(112,128,144,1)"
        static member Snow with get() = unbox<Color> "rgba(255,250,250,1)"
        static member SpringGreen with get() = unbox<Color> "rgba(0,255,127,1)"
        static member SteelBlue with get() = unbox<Color> "rgba(70,130,180,1)"
        static member Tan with get() = unbox<Color> "rgba(210,180,140,1)"
        static member Teal with get() = unbox<Color> "rgba(0,128,128,1)"
        static member Thistle with get() = unbox<Color> "rgba(216,191,216,1)"
        static member Tomato with get() = unbox<Color> "rgba(255,99,71,1)"
        static member Transparent with get() = unbox<Color> "rgba(255,255,255,0)"
        static member Turquoise with get() = unbox<Color> "rgba(64,224,208,1)"
        static member Violet with get() = unbox<Color> "rgba(238,130,238,1)"
        static member Wheat with get() = unbox<Color> "rgba(245,222,179,1)"
        static member White with get() = unbox<Color> "rgba(255,255,255,1)"
        static member WhiteSmoke with get() = unbox<Color> "rgba(245,245,245,1)"
        static member Yellow with get() = unbox<Color> "rgba(255,255,0,1)"
        static member YellowGreen with get() = unbox<Color> "rgba(154,205,50,1)"

    type Pens =
        static member AliceBlue with get() = Pen(unbox<Color> "rgba(240,248,255,1)")
        static member AntiqueWhite with get() = Pen(unbox<Color> "rgba(250,235,215,1)")
        static member Aqua with get() = Pen(unbox<Color> "rgba(0,255,255,1)")
        static member Aquamarine with get() = Pen(unbox<Color> "rgba(127,255,212,1)")
        static member Azure with get() = Pen(unbox<Color> "rgba(240,255,255,1)")
        static member Beige with get() = Pen(unbox<Color> "rgba(245,245,220,1)")
        static member Bisque with get() = Pen(unbox<Color> "rgba(255,228,196,1)")
        static member Black with get() = Pen(unbox<Color> "rgba(0,0,0,1)")
        static member BlanchedAlmond with get() = Pen(unbox<Color> "rgba(255,235,205,1)")
        static member Blue with get() = Pen(unbox<Color> "rgba(0,0,255,1)")
        static member BlueViolet with get() = Pen(unbox<Color> "rgba(138,43,226,1)")
        static member Brown with get() = Pen(unbox<Color> "rgba(165,42,42,1)")
        static member BurlyWood with get() = Pen(unbox<Color> "rgba(222,184,135,1)")
        static member CadetBlue with get() = Pen(unbox<Color> "rgba(95,158,160,1)")
        static member Chartreuse with get() = Pen(unbox<Color> "rgba(127,255,0,1)")
        static member Chocolate with get() = Pen(unbox<Color> "rgba(210,105,30,1)")
        static member Coral with get() = Pen(unbox<Color> "rgba(255,127,80,1)")
        static member CornflowerBlue with get() = Pen(unbox<Color> "rgba(100,149,237,1)")
        static member Cornsilk with get() = Pen(unbox<Color> "rgba(255,248,220,1)")
        static member Crimson with get() = Pen(unbox<Color> "rgba(220,20,60,1)")
        static member Cyan with get() = Pen(unbox<Color> "rgba(0,255,255,1)")
        static member DarkBlue with get() = Pen(unbox<Color> "rgba(0,0,139,1)")
        static member DarkCyan with get() = Pen(unbox<Color> "rgba(0,139,139,1)")
        static member DarkGoldenrod with get() = Pen(unbox<Color> "rgba(184,134,11,1)")
        static member DarkGray with get() = Pen(unbox<Color> "rgba(169,169,169,1)")
        static member DarkGreen with get() = Pen(unbox<Color> "rgba(0,100,0,1)")
        static member DarkKhaki with get() = Pen(unbox<Color> "rgba(189,183,107,1)")
        static member DarkMagenta with get() = Pen(unbox<Color> "rgba(139,0,139,1)")
        static member DarkOliveGreen with get() = Pen(unbox<Color> "rgba(85,107,47,1)")
        static member DarkOrange with get() = Pen(unbox<Color> "rgba(255,140,0,1)")
        static member DarkOrchid with get() = Pen(unbox<Color> "rgba(153,50,204,1)")
        static member DarkRed with get() = Pen(unbox<Color> "rgba(139,0,0,1)")
        static member DarkSalmon with get() = Pen(unbox<Color> "rgba(233,150,122,1)")
        static member DarkSeaGreen with get() = Pen(unbox<Color> "rgba(143,188,143,1)")
        static member DarkSlateBlue with get() = Pen(unbox<Color> "rgba(72,61,139,1)")
        static member DarkSlateGray with get() = Pen(unbox<Color> "rgba(47,79,79,1)")
        static member DarkTurquoise with get() = Pen(unbox<Color> "rgba(0,206,209,1)")
        static member DarkViolet with get() = Pen(unbox<Color> "rgba(148,0,211,1)")
        static member DeepPink with get() = Pen(unbox<Color> "rgba(255,20,147,1)")
        static member DeepSkyBlue with get() = Pen(unbox<Color> "rgba(0,191,255,1)")
        static member DimGray with get() = Pen(unbox<Color> "rgba(105,105,105,1)")
        static member DodgerBlue with get() = Pen(unbox<Color> "rgba(30,144,255,1)")
        static member Firebrick with get() = Pen(unbox<Color> "rgba(178,34,34,1)")
        static member FloralWhite with get() = Pen(unbox<Color> "rgba(255,250,240,1)")
        static member ForestGreen with get() = Pen(unbox<Color> "rgba(34,139,34,1)")
        static member Fuchsia with get() = Pen(unbox<Color> "rgba(255,0,255,1)")
        static member Gainsboro with get() = Pen(unbox<Color> "rgba(220,220,220,1)")
        static member GhostWhite with get() = Pen(unbox<Color> "rgba(248,248,255,1)")
        static member Gold with get() = Pen(unbox<Color> "rgba(255,215,0,1)")
        static member Goldenrod with get() = Pen(unbox<Color> "rgba(218,165,32,1)")
        static member Gray with get() = Pen(unbox<Color> "rgba(128,128,128,1)")
        static member Green with get() = Pen(unbox<Color> "rgba(0,128,0,1)")
        static member GreenYellow with get() = Pen(unbox<Color> "rgba(173,255,47,1)")
        static member Honeydew with get() = Pen(unbox<Color> "rgba(240,255,240,1)")
        static member HotPink with get() = Pen(unbox<Color> "rgba(255,105,180,1)")
        static member IndianRed with get() = Pen(unbox<Color> "rgba(205,92,92,1)")
        static member Indigo with get() = Pen(unbox<Color> "rgba(75,0,130,1)")
        static member Ivory with get() = Pen(unbox<Color> "rgba(255,255,240,1)")
        static member Khaki with get() = Pen(unbox<Color> "rgba(240,230,140,1)")
        static member Lavender with get() = Pen(unbox<Color> "rgba(230,230,250,1)")
        static member LavenderBlush with get() = Pen(unbox<Color> "rgba(255,240,245,1)")
        static member LawnGreen with get() = Pen(unbox<Color> "rgba(124,252,0,1)")
        static member LemonChiffon with get() = Pen(unbox<Color> "rgba(255,250,205,1)")
        static member LightBlue with get() = Pen(unbox<Color> "rgba(173,216,230,1)")
        static member LightCoral with get() = Pen(unbox<Color> "rgba(240,128,128,1)")
        static member LightCyan with get() = Pen(unbox<Color> "rgba(224,255,255,1)")
        static member LightGoldenrodYellow with get() = Pen(unbox<Color> "rgba(250,250,210,1)")
        static member LightGray with get() = Pen(unbox<Color> "rgba(211,211,211,1)")
        static member LightGreen with get() = Pen(unbox<Color> "rgba(144,238,144,1)")
        static member LightPink with get() = Pen(unbox<Color> "rgba(255,182,193,1)")
        static member LightSalmon with get() = Pen(unbox<Color> "rgba(255,160,122,1)")
        static member LightSeaGreen with get() = Pen(unbox<Color> "rgba(32,178,170,1)")
        static member LightSkyBlue with get() = Pen(unbox<Color> "rgba(135,206,250,1)")
        static member LightSlateGray with get() = Pen(unbox<Color> "rgba(119,136,153,1)")
        static member LightSteelBlue with get() = Pen(unbox<Color> "rgba(176,196,222,1)")
        static member LightYellow with get() = Pen(unbox<Color> "rgba(255,255,224,1)")
        static member Lime with get() = Pen(unbox<Color> "rgba(0,255,0,1)")
        static member LimeGreen with get() = Pen(unbox<Color> "rgba(50,205,50,1)")
        static member Linen with get() = Pen(unbox<Color> "rgba(250,240,230,1)")
        static member Magenta with get() = Pen(unbox<Color> "rgba(255,0,255,1)")
        static member Maroon with get() = Pen(unbox<Color> "rgba(128,0,0,1)")
        static member MediumAquamarine with get() = Pen(unbox<Color> "rgba(102,205,170,1)")
        static member MediumBlue with get() = Pen(unbox<Color> "rgba(0,0,205,1)")
        static member MediumOrchid with get() = Pen(unbox<Color> "rgba(186,85,211,1)")
        static member MediumPurple with get() = Pen(unbox<Color> "rgba(147,112,219,1)")
        static member MediumSeaGreen with get() = Pen(unbox<Color> "rgba(60,179,113,1)")
        static member MediumSlateBlue with get() = Pen(unbox<Color> "rgba(123,104,238,1)")
        static member MediumSpringGreen with get() = Pen(unbox<Color> "rgba(0,250,154,1)")
        static member MediumTurquoise with get() = Pen(unbox<Color> "rgba(72,209,204,1)")
        static member MediumVioletRed with get() = Pen(unbox<Color> "rgba(199,21,133,1)")
        static member MidnightBlue with get() = Pen(unbox<Color> "rgba(25,25,112,1)")
        static member MintCream with get() = Pen(unbox<Color> "rgba(245,255,250,1)")
        static member MistyRose with get() = Pen(unbox<Color> "rgba(255,228,225,1)")
        static member Moccasin with get() = Pen(unbox<Color> "rgba(255,228,181,1)")
        static member NavajoWhite with get() = Pen(unbox<Color> "rgba(255,222,173,1)")
        static member Navy with get() = Pen(unbox<Color> "rgba(0,0,128,1)")
        static member OldLace with get() = Pen(unbox<Color> "rgba(253,245,230,1)")
        static member Olive with get() = Pen(unbox<Color> "rgba(128,128,0,1)")
        static member OliveDrab with get() = Pen(unbox<Color> "rgba(107,142,35,1)")
        static member Orange with get() = Pen(unbox<Color> "rgba(255,165,0,1)")
        static member OrangeRed with get() = Pen(unbox<Color> "rgba(255,69,0,1)")
        static member Orchid with get() = Pen(unbox<Color> "rgba(218,112,214,1)")
        static member PaleGoldenrod with get() = Pen(unbox<Color> "rgba(238,232,170,1)")
        static member PaleGreen with get() = Pen(unbox<Color> "rgba(152,251,152,1)")
        static member PaleTurquoise with get() = Pen(unbox<Color> "rgba(175,238,238,1)")
        static member PaleVioletRed with get() = Pen(unbox<Color> "rgba(219,112,147,1)")
        static member PapayaWhip with get() = Pen(unbox<Color> "rgba(255,239,213,1)")
        static member PeachPuff with get() = Pen(unbox<Color> "rgba(255,218,185,1)")
        static member Peru with get() = Pen(unbox<Color> "rgba(205,133,63,1)")
        static member Pink with get() = Pen(unbox<Color> "rgba(255,192,203,1)")
        static member Plum with get() = Pen(unbox<Color> "rgba(221,160,221,1)")
        static member PowderBlue with get() = Pen(unbox<Color> "rgba(176,224,230,1)")
        static member Purple with get() = Pen(unbox<Color> "rgba(128,0,128,1)")
        static member Red with get() = Pen(unbox<Color> "rgba(255,0,0,1)")
        static member RosyBrown with get() = Pen(unbox<Color> "rgba(188,143,143,1)")
        static member RoyalBlue with get() = Pen(unbox<Color> "rgba(65,105,225,1)")
        static member SaddleBrown with get() = Pen(unbox<Color> "rgba(139,69,19,1)")
        static member Salmon with get() = Pen(unbox<Color> "rgba(250,128,114,1)")
        static member SandyBrown with get() = Pen(unbox<Color> "rgba(244,164,96,1)")
        static member SeaGreen with get() = Pen(unbox<Color> "rgba(46,139,87,1)")
        static member SeaShell with get() = Pen(unbox<Color> "rgba(255,245,238,1)")
        static member Sienna with get() = Pen(unbox<Color> "rgba(160,82,45,1)")
        static member Silver with get() = Pen(unbox<Color> "rgba(192,192,192,1)")
        static member SkyBlue with get() = Pen(unbox<Color> "rgba(135,206,235,1)")
        static member SlateBlue with get() = Pen(unbox<Color> "rgba(106,90,205,1)")
        static member SlateGray with get() = Pen(unbox<Color> "rgba(112,128,144,1)")
        static member Snow with get() = Pen(unbox<Color> "rgba(255,250,250,1)")
        static member SpringGreen with get() = Pen(unbox<Color> "rgba(0,255,127,1)")
        static member SteelBlue with get() = Pen(unbox<Color> "rgba(70,130,180,1)")
        static member Tan with get() = Pen(unbox<Color> "rgba(210,180,140,1)")
        static member Teal with get() = Pen(unbox<Color> "rgba(0,128,128,1)")
        static member Thistle with get() = Pen(unbox<Color> "rgba(216,191,216,1)")
        static member Tomato with get() = Pen(unbox<Color> "rgba(255,99,71,1)")
        static member Transparent with get() = Pen(unbox<Color> "rgba(255,255,255,0)")
        static member Turquoise with get() = Pen(unbox<Color> "rgba(64,224,208,1)")
        static member Violet with get() = Pen(unbox<Color> "rgba(238,130,238,1)")
        static member Wheat with get() = Pen(unbox<Color> "rgba(245,222,179,1)")
        static member White with get() = Pen(unbox<Color> "rgba(255,255,255,1)")
        static member WhiteSmoke with get() = Pen(unbox<Color> "rgba(245,245,245,1)")
        static member Yellow with get() = Pen(unbox<Color> "rgba(255,255,0,1)")
        static member YellowGreen with get() = Pen(unbox<Color> "rgba(154,205,50,1)")


    