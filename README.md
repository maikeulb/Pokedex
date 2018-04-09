# PokedexES

Pokedex search engine that utilizes Elasticsearch to query (full-text multi-match)
indexed pokemon data. The pokemon are ranked based on their relevancy.

Technology
----------
* ASP.NET Core 2.0
* Elasticsearch 
* NEST
* Sakura CSS

Screenshot
---
![poke](/screenshots/poke.png?raw=true "Post")

Run
---

You need .NET Core 2.0 SDK and a local Elastisearch service to run the
application. If you meeet those requirements, then  open
`/Data/ElasticSearchContext.cs` and point the Elasticsearch URI to your client.
```
dotnet restore
dotnet run
Go to http://localhost:5000
```
TODO
----
Fix Dockerfile
