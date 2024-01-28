# Description of the Universe.Lastfm.API

Here is a project of a Last.fm api, the purpose of which is to simplify the development of solutions 
in the C# programming language, such as scrobbler for example.

# Version control    
For convenience, the project should be cloned to the local path C:\P\Universe.Lastfm.API\ (but this is not at all necessary)
* master is the main working branch, changes are made to it only through MergeRequests
* develop - the second main working branch, changes are also included in it through MergeRequests

A separate branch is created for each development/bug fix task.

*It is created directly from the task, it is necessary to include errors in the name of the task; before creating it, indicate in parentheses in English the not very long name of the branch.*

## Rules for generating messages to a commit
The message also can be multi-line, for example:
#89 [WebApp Auth]: Added authorization in the web application.
#89 [Common]: Changed links to projects.
#89 [Universe.Algorithm, Tests]: Removed artifact. 

where:
* #89 - issue number (usually the same as the branch number) - in gitlab it will turn into a link to the issue,
   and when you hover the mouse it will show the name of the task
* "[WebApp Auth]:"" name of the functionality within which the commit is made. Must be indicated with a colon at the end.
* "Authorization has been added to the web application." - text describing what was done. Be sure to have a period at the end to complete the sentence.
* Several actions can be listed that are recorded in this manner
   e.g. "#89 WebApp Auth: The Unity container is connected in the WebApp project. The Unity container is connected in the Core project.""

* [\~] - we indicate it at the beginning of the commit line if we are merging files manually (git automatically generates a message). The message should look like:
     [\~] Merge from develop to 20-build-ef-data-access-layer
     or
     [\~] Merge from develop to #20

# Development tools
For a development, you should use VS 2017, VS2019, VS2022; also must additionally be installed:
* support for PowerShell projects (this is installed when installing VS)
* git integrated into studio (this is installed when installing VS)
* If, after updating from the develop branch, the projects' References are gone, and the error shows "NuGet", you can do the following:
    on Solution, right-click and select and select Restore NuGet Packages. Then Clean solution, build solution.
* To have NuGet restore assemblies automatically, you need to do the following:
    in the Tools - Options - NuGet Package Manager menu, select 2 checkboxes:
    - Allow NuGet to download missing packages;
    - Automaticall check for missing packages during build in Visual Studio.

# Note
* File encoding (especially *.ps1) must be UTF-8

# CodingStyle
* When formatting the code, follow the resharper settings in the ReSharper.DotSettings file
* !Before committing, be sure to reformat the changed code according to the ResharperSln scheme (exceptions are codogen and code
  generated by T4 template)
* When reformating, select the ResharperSln profile for a full reformat, ResharperSln NoSort for classes in which the order cannot be changed
* Catch block, if there is no `throw ...;` in the catch block, then you need to indicate a comment why it is not there, for example
  as in the example below
  If a new error instance is created in the catch block, then it is necessary to indicate the original error or comment
  why the original error should not be stated.
```c#
catch (Exception ex) {
    _log.Unexpected(ex);
    //throw; Whatever happens here should not affect the execution of everything else
}
````

# Examples of using ...
## Formation of samples according to specific conditions

The project uses a whole arsenal of filters for more convenient and flexible creation of database queries.
Below is an examples of use.

Gets a track information:

```c#

    string trackName = "Age Of Shadows";
    string performer = "Ayreon";

    IUnityContainer container = UnityAppConfig.Container;
    UniverseLastApiScope scope = container.Resolve<IUniverseScope>() as UniverseLastApiScope;

    ReqContext reqCtx = new ReqContext();
    reqCtx.Track = trackName;
    reqCtx.Performer = performer;

    GetTrackInfoResponce responce = scope.GetQuery<GetTrackInfoQuery>().Execute(reqCtx.As<GetTrackInfoRequest>());
    TrackFull track = responce.DataContainer.Track;

````

where: 
   * container - the DI-container, that helps for an implenmentation of dependency ingections for your project 
                  and also storages was created before instances of classes. Important: you need to add UnityAppConfig 
                  as project 'Universe.Lastfm.Api.FormsApp' that lies in the folder 'Infrastracture';

   * scope - the object, which could execute queries and commands. This is an activity sphere other words;
   * reqCtx - the object was created for a development comfort. Inherits from object 'BaseRequest'. In this case replaced 
               request usage was implemented in the class 'GetTrackInfoRequest'. Of course you might use the class 
               GetTrackInfoRequest. The class GetTrackInfoRequest definates parameters 'TrackName' and 'Performer'.
               This is required parameters and they will be described below;

   * trackName - the track name;
   * performer - the artist name;

   * GetQuery<GetTrackInfoQuery>().Execute( - this is a method that generates a request and
				which takes GetTrackInfoRequest (BaseRequest) as an argument containing required parameters.
                In this case gets the metadata for a track on Last.fm using the artist/track name or a musicbrainz id;

   * responce - the responce with full information about track on the Last.fm. Inferites from BaseResponce;
   * track - the track information model. You might get it from the property 'DataContainer' that is deserializing 
              property 'ServiceAnswer'.

Marks a track as the loved track:

```c#

    string trackName = "Age Of Shadows";
    string performer = "Ayreon";

    IUnityContainer container = UnityAppConfig.Container;
    UniverseLastApiScope scope = container.Resolve<IUniverseScope>() as UniverseLastApiScope;

    ReqContext reqCtx = new ReqContext();
    reqCtx.Track = trackName;
    reqCtx.Performer = performer;

    UpdateTrackAsLoveCommandResponce responce = Scope.GetCommand<UpdateTrackAsLoveCommand>().Execute(ReqCtx.As<UpdateTrackAsLoveRequest>());
    var serviceAnswer = responce.ServiceAnswer;

````

where: 
   * container - the DI-container, that helps for an implenmentation of dependency ingections for your project 
                  and also storages was created before instances of classes. Important: you need to add UnityAppConfig 
                  as project 'Universe.Lastfm.Api.FormsApp' that lies in the folder 'Infrastracture';

   * scope - the object, which could execute queries and commands. This is an activity sphere other words;
   * reqCtx - the object was created for a development comfort. Inherits from object 'BaseRequest'. In this case replaced 
               request usage was implemented in the class 'UpdateTrackAsLoveRequest'. Of course you might use the class 
               UpdateTrackAsLoveRequest. The class UpdateTrackAsLoveRequest definates parameters 'TrackName' and 'Performer'.
               This is required parameters and they will be described below;

   * trackName - the track name;
   * performer - the artist name;

   * GetQuery<UpdateTrackAsLoveCommand>().Execute( - this is a method that generates a request and
				which takes UpdateTrackAsLoveRequest (BaseRequest) as an argument containing required parameters.
                In this case gets the metadata for a track on Last.fm using the artist/track name or a musicbrainz id;

   * responce - the responce with answer of the command. Inferites from BaseResponce;
   * track - the track information model. You might get it from the property 'DataContainer' that is deserializing 
              property 'ServiceAnswer'.

Gets an album information:

```c#

    string performer = "Ayreon";
    string album = "01011001";

    IUnityContainer container = UnityAppConfig.Container;
    UniverseLastApiScope scope = container.Resolve<IUniverseScope>() as UniverseLastApiScope;

    ReqContext reqCtx = new ReqContext();
    reqCtx.Album = album;
    reqCtx.Performer = performer;

    GetAlbumInfoResponce responce = scope.GetQuery<GetAlbumInfoQuery>().Execute(reqCtx.As<GetAlbumInfoRequest>());
    Album albumInfo = responce.DataContainer.Album;

````

where: 
   * container - the DI-container, that helps for an implenmentation of dependency ingections for your project 
                  and also storages was created before instances of classes. Important: you need to add UnityAppConfig 
                  as project 'Universe.Lastfm.Api.FormsApp' that lies in the folder 'Infrastracture';

   * scope - the object, which could execute queries and commands. This is an activity sphere other words;
   * reqCtx - the object was created for a development comfort. Inherits from object 'BaseRequest'. In this case replaced 
               request usage was implemented in the class 'GetAlbumInfoRequest'. Of course you might use the class 
               GetAlbumInfoRequest. The class GetAlbumInfoRequest definates parameters 'TrackName' and 'Performer'.
               This is required parameters and they will be described below;

   * album - the album name;
   * performer - the artist name;

   * GetQuery<GetAlbumInfoRequest>().Execute( - this is a method that generates a request and
				which takes GetAlbumInfoRequest (BaseRequest) as an argument containing required parameters.
                In this case gets the metadata for a album on Last.fm using the artist/album name or a musicbrainz id;

   * responce - the responce with full information about track on the Last.fm. Inferites from BaseResponce;
   * albumInfo - the album information model. You might get it from the property 'DataContainer' that is deserializing 
              property 'ServiceAnswer'.