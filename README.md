# TheLibraryIsOpen
SOEN 343 Project (Fall 2018)

Team 2 

Name,  Student Id,  Email,  Github Username

1. Anamika Pancholy,  27844263,  anamika.pancholy@gmail.com,  MickeyPa

2. Sandra Buchen,  26317987,  s.buchen22@gmail.com,  sandraroz

3. Matthew Benchimol,  27759614,  matthewbenchimol@hotmail.com,  MBenchimol

4. Yefei Xue,  26433979,  felixyf0124@gmail.com,  felixyf0124

5. Albert Najjar,  27914393,  albertnaggear@gmail.com,  Aelgorn

6. Kevin Lo,  40032509,  lo_kevin@outlook.com,  lokevin88

7. Li Sun,  40017648,  vincent.sun870530@gmail.com,  vincentsun870530

8. Jessica Allaire,  40015912,  jessica.allaire.96@hotmail.com,  tirafire

9. Antoine Betenjaneh,  27161956,  antoine.beiten@gmail.com,  soenAnt

10. Xi Chen,  27276605,  davidseechan@gmail.com,  g82005
    * I am including github usernames since some of the account emails do not match the ones submitted to the TA.

## Compilation
To compile, the .net core runtime must be installed on the host computer.
It's easier to have visual studio 2017 with the .net core packages installed to run it. However, it can still be done via console:

1) cd to the root folder of the project and run the command `dotnet build --configuration Release`.
2) navigate to `~\TheLibraryIsOpen` and copy the `wwwroot` folder and `appsettings.json`, and paste them into `~\TheLibraryIsOpen\bin\Release\netcoreapp2.1`
3) cd to `~\TheLibraryIsOpen\bin\Release\netcoreapp2.1` and run the command `dotnet TheLibraryIsOpen.dll`
4) the console will show a few messages, among which:
    * "Now listening on: http://<span></span>localhost:[portnb1]"
    * "Now listening on: https://<span></span>localhost:[portnb2]"
5) pick whichever link and put it into a browser
    
## Admin account
* username: admin@thelibraryisopen<span></span>.com
* password: admin

## Search bar
You can search the items by entering the search string of almost any field of that item. In most cases (except ones where precision is required, such as Asin or ISBN identifiers), the engine will look for fields containing the input string as a substring.
* If you want your search to be more specific, you can use the `;` character in your input.
    >i.e: "Harry Potter; 2003" will search for all objects with the substring "Harry Potter" (which could be in this example in the title field) AND the substring "2003" (which could be in this example in the date field).