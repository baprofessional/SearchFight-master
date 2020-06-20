# SearchFight
***********Note as of no i have hard coded the values of google count since the id value is not getting from response *********
// var node = htmlDoc.GetElementbyId("resultStats");
// var content = node.InnerText;
     //Hard coded belowhere since on respense cant find the element count value on this line of code var node = 
        /// var content = "6,42,0000"
        ***************************************************
        
Console application for comparing the result quantity of Google and Bing search engine

To determine the popularity of programming languages on the internet we want to you to write an application that queries search engines and compares how many results they return, for example:

    C:\> searchfight.exe .net java
    .net: Google: 4450000000 MSN Search: 12354420
    java: Google: 966000000 MSN Search: 94381485
    Google winner: .net
    MSN Search winner: java
    Total winner: .net

- The application should be able to receive a variable amount of words
- The application should support quotation marks to allow searching for terms with spaces (e.g. searchfight.exe “java script”)
- The application should support at least two search engines
