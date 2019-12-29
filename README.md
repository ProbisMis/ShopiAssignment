## ShopiAssignment ##

### Installation ###

* type `git clone https://github.com/ProbisMis/ShopiAssignment.git projectname` to clone the repository 
* type `cd projectname`
* open `WebApplication1.sln` file with Visual Studio 2019
* Run Project

### API ###

* https://localhost:44319/data -> will parse sample.csv and creates product list (returns => json)
* https://localhost:44319/data/create -> will post product list to server (returns server response)


### Packages ###

* [CsvHelper] forparsing  `WebApplication1/App_Data/sampe.csv`
* [Newtonsoft Json] for serializing Product object

