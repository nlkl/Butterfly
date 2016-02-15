namespace Butterfly.Tests

open Xunit
open FsUnit.Xunit

module ``Field Tests`` = 

    [<Fact>]
    let ``Can retrieve raw value``() = true |> should be True
