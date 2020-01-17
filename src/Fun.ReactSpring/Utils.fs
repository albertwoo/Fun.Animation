module Fun.ReactSpring.Utils

open Fable.Core


[<Emit("(...args) => $0(args)")>]
let mapJsArgs mapper: 'Args -> 'T = jsNative


[<Emit("$0['$1']")>]
let getJsValueByKey obj key: string = jsNative


[<Emit("
Object.keys($0).map(x => {
    return [x, $0[x]];
});
")>]
let jsObjKeyValues obj: (string * obj)[] = jsNative
