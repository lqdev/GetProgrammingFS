type Command = 
| Widthdraw
| Deposit
| Exit

let tryParseCommand input = 
    match input with
    | 'w' -> Some Widthdraw
    | 'd' -> Some Deposit
    | 'x' -> Some Exit
    | _ -> None

let exit = ['x'] |> List.choose tryParseCommand

let isValidCommand cmd = tryParseCommand cmd