let text = "Hello World!"
text.Length

let greetPerson name age = 
    sprintf "Hello %s. You are %d years old" name age
   
let greeting = greetPerson "Fred" 25

let countWords(text: string) = 
    let words = text.Split[|' '|]
    words.Length

printfn "%d" (countWords("Hello World"))
    