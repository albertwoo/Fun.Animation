[<RequireQualifiedAccess>]
module Fun.ReactSpring.Animated

open Fable.React
open Fable.React.Isomorphic
open Utils
open Bindings


let inline animatedEle ty props childs =
  let ty =
    isomorphicExec
        (fun () -> getJsValueByKey animatedEles ty)
        (fun () -> ty)
        ()
  domEl ty props childs


let inline a props childs = animatedEle "a" props childs
let inline abbr props childs = animatedEle "abbr" props childs
let inline address props childs = animatedEle "address" props childs
let inline area props childs = animatedEle "area" props childs
let inline article props childs = animatedEle "article" props childs
let inline aside props childs = animatedEle "aside" props childs
let inline audio props childs = animatedEle "audio" props childs
let inline b props childs = animatedEle "b" props childs
let inline bdi props childs = animatedEle "bdi" props childs
let inline bdo props childs = animatedEle "bdo" props childs
let inline big props childs = animatedEle "big" props childs
let inline blockquote props childs = animatedEle "blockquote" props childs
let inline body props childs = animatedEle "body" props childs
let inline br props childs = animatedEle "br" props childs
let inline button props childs = animatedEle "button" props childs
let inline canvas props childs = animatedEle "canvas" props childs
let inline caption props childs = animatedEle "caption" props childs
let inline cite props childs = animatedEle "cite" props childs
let inline code props childs = animatedEle "code" props childs
let inline col props childs = animatedEle "col" props childs
let inline colgroup props childs = animatedEle "colgroup" props childs
let inline data props childs = animatedEle "data" props childs
let inline datalist props childs = animatedEle "datalist" props childs
let inline dd props childs = animatedEle "dd" props childs
let inline del props childs = animatedEle "del" props childs
let inline details props childs = animatedEle "details" props childs
let inline dfn props childs = animatedEle "dfn" props childs
let inline dialog props childs = animatedEle "dialog" props childs
let inline div props childs = animatedEle "div" props childs
let inline dl props childs = animatedEle "dl" props childs
let inline dt props childs = animatedEle "dt" props childs
let inline em props childs = animatedEle "em" props childs
let inline embed props childs = animatedEle "embed" props childs
let inline fieldset props childs = animatedEle "fieldset" props childs
let inline figcaption props childs = animatedEle "figcaption" props childs
let inline figure props childs = animatedEle "figure" props childs
let inline footer props childs = animatedEle "footer" props childs
let inline form props childs = animatedEle "form" props childs
let inline h1 props childs = animatedEle "h1" props childs
let inline h2 props childs = animatedEle "h2" props childs
let inline h3 props childs = animatedEle "h3" props childs
let inline h4 props childs = animatedEle "h4" props childs
let inline h5 props childs = animatedEle "h5" props childs
let inline h6 props childs = animatedEle "h6" props childs
let inline head props childs = animatedEle "head" props childs
let inline header props childs = animatedEle "header" props childs
let inline hgroup props childs = animatedEle "hgroup" props childs
let inline hr props childs = animatedEle "hr" props childs
let inline html props childs = animatedEle "html" props childs
let inline i props childs = animatedEle "i" props childs
let inline iframe props childs = animatedEle "iframe" props childs
let inline img props childs = animatedEle "img" props childs
let inline input props childs = animatedEle "input" props childs
let inline ins props childs = animatedEle "ins" props childs
let inline kbd props childs = animatedEle "kbd" props childs
let inline keygen props childs = animatedEle "keygen" props childs
let inline label props childs = animatedEle "label" props childs
let inline legend props childs = animatedEle "legend" props childs
let inline li props childs = animatedEle "li" props childs
let inline link props childs = animatedEle "link" props childs
let inline main props childs = animatedEle "main" props childs
let inline map props childs = animatedEle "map" props childs
let inline mark props childs = animatedEle "mark" props childs
let inline marquee props childs = animatedEle "marquee" props childs
let inline menu props childs = animatedEle "menu" props childs
let inline menuitem props childs = animatedEle "menuitem" props childs
let inline meta props childs = animatedEle "meta" props childs
let inline meter props childs = animatedEle "meter" props childs
let inline nav props childs = animatedEle "nav" props childs
let inline noscript props childs = animatedEle "noscript" props childs
let inline object props childs = animatedEle "object" props childs
let inline ol props childs = animatedEle "ol" props childs
let inline optgroup props childs = animatedEle "optgroup" props childs
let inline option props childs = animatedEle "option" props childs
let inline output props childs = animatedEle "output" props childs
let inline p props childs = animatedEle "p" props childs
let inline param props childs = animatedEle "param" props childs
let inline picture props childs = animatedEle "picture" props childs
let inline pre props childs = animatedEle "pre" props childs
let inline progress props childs = animatedEle "progress" props childs
let inline q props childs = animatedEle "q" props childs
let inline rp props childs = animatedEle "rp" props childs
let inline rt props childs = animatedEle "rt" props childs
let inline ruby props childs = animatedEle "ruby" props childs
let inline s props childs = animatedEle "s" props childs
let inline samp props childs = animatedEle "samp" props childs
let inline script props childs = animatedEle "script" props childs
let inline section props childs = animatedEle "section" props childs
let inline select props childs = animatedEle "select" props childs
let inline small props childs = animatedEle "small" props childs
let inline source props childs = animatedEle "source" props childs
let inline span props childs = animatedEle "span" props childs
let inline strong props childs = animatedEle "strong" props childs
let inline style props childs = animatedEle "style" props childs
let inline sub props childs = animatedEle "sub" props childs
let inline summary props childs = animatedEle "summary" props childs
let inline sup props childs = animatedEle "sup" props childs
let inline table props childs = animatedEle "table" props childs
let inline tbody props childs = animatedEle "tbody" props childs
let inline td props childs = animatedEle "td" props childs
let inline textarea props childs = animatedEle "textarea" props childs
let inline tfoot props childs = animatedEle "tfoot" props childs
let inline th props childs = animatedEle "th" props childs
let inline thead props childs = animatedEle "thead" props childs
let inline time props childs = animatedEle "time" props childs
let inline title props childs = animatedEle "title" props childs
let inline tr props childs = animatedEle "tr" props childs
let inline track props childs = animatedEle "track" props childs
let inline u props childs = animatedEle "u" props childs
let inline ul props childs = animatedEle "ul" props childs
let inline var props childs = animatedEle "var" props childs
let inline video props childs = animatedEle "video" props childs
let inline wbr props childs = animatedEle "wbr" props childs
let inline circle props childs = animatedEle "circle" props childs
let inline clipPath props childs = animatedEle "clipPath" props childs
let inline defs props childs = animatedEle "defs" props childs
let inline ellipse props childs = animatedEle "ellipse" props childs
let inline foreignObject props childs = animatedEle "foreignObject" props childs
let inline g props childs = animatedEle "g" props childs
let inline image props childs = animatedEle "image" props childs
let inline line props childs = animatedEle "line" props childs
let inline linearGradient props childs = animatedEle "linearGradient" props childs
let inline mask props childs = animatedEle "mask" props childs
let inline path props childs = animatedEle "path" props childs
let inline pattern props childs = animatedEle "pattern" props childs
let inline polygon props childs = animatedEle "polygon" props childs
let inline polyline props childs = animatedEle "polyline" props childs
let inline radialGradient props childs = animatedEle "radialGradient" props childs
let inline rect props childs = animatedEle "rect" props childs
let inline stop props childs = animatedEle "stop" props childs
let inline svg props childs = animatedEle "svg" props childs
let inline text props childs = animatedEle "text" props childs
let inline tspan props childs = animatedEle "tspan" props childs
