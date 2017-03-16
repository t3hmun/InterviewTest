# Manish Parekh Interview Test

I succeed in dynamically pulling the funds managed by each manager and displaying them on the Razor view.

Please see the [Closed issues](https://github.com/t3hmun/InterviewTest/issues?q=is%3Aissue+is%3Aclosed)
for how I partitioned the work.

There is the [diff between our repositories](https://github.com/FundsLibrary/InterviewTest/compare/master...t3hmun:master), that is obvious in the pull request anyway.


`secrets.config` would normally be gitignored, but for this Interview-test it made sens to include it.

![This looks like success](/Success.jpg)


## Issues I had with FundsLibrary

I made test queries with Insomnia (its an app similar to PostMan).

I couldn't make the FundsLibrary server accept any OData `$select` or `any(item:item ...)` queries.
This lead me to rely on using `$search` on the manager Guid, rather inefficient.

I didn't want to page the whole OData server and cache the data, I felt this app demanded up-to-date queries.

My queries haven't found more than one fund per manager so I'd want to look into my query to see if I made a mistake.


## Things I Learned

* Castle Windsor
    * I've other Dependency Injection / IoC frameworks so it wasn't hard
* Moq
* OData queries (though in future I'd use  a OData-Linq library).


## Things I Didn't Have Time to Do

I'd usually want to do all these things, but I am pressed for time.

* Check Nuget packages for security updates.
* Write tests before writing new code.
    * I need some time to learn good TDD.
* Handle failed requests properly gracefully.
* Generate a nice model to make all the data accessible
    * It'd be a large amount of files, not worth it for a small project.
* Make the FundsLibrary logo look sensible / improve all aesthetics