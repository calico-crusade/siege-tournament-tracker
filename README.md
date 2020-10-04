# Rainbow Six Siege Match / Tournament Tracker
Displays a feed of upcoming and recent matches for R6 Pro League (PL) and Challenger League (CL)

The application was written in C# using asp.net core as the middleware and Angular 10.1 (TypeScript, SCSS and HTML) as the frontend.

## Endpoints
Here is a collection of web-api endpoints and other useful URLs for the application. 

* [MatchesController](/SiegeTournamentTracker.Web/Controllers/Api/MatchesController.cs) - Fetches match data
    * `GET /api/matches/all` - Fetches all of the upcoming and recent matches. 
    * `GET /api/matches/upcoming` - Fetches only the upcoming matches.
* [MetaDataController](/SiegeTournamentTracker.Web/Controllers/Api/MetaDataController.cs) - Fetches meta data used for displays on the UI
    * `GET /api/metadata/matchstatus?local=en` - Fetches meta data about the `MatchStatus` flag on the matches
    * `GET /api/metadata/image?url=<image-url>` - Fetches a given image from the liquipedia image cache
    * `GET /api/metadata/leagues` - Fetches a list of all of the leagues currently in the `/api/matches/all` endpoint
* [Angular Routes](siege-tour-tracker/src/app/app-routing.module.ts) - Client side routes
    * `/matches` - Displays all of the upcoming and recent matches with a helpful-ish filter/search feature
    * `/**` - Not found component (not implemented yet)
    * `/` - Redirects to `/matches`
* MVC Routes - Server side routes
    * `/swagger` - Built in documentation using Swashbuckle / Swagger
    * `**` - SPA redirect to the angular routes
     
