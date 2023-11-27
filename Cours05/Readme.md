# TodAPI

👋 **Bonjour l'équipe SysNet**,

Lors de notre dernier exo, nous avons créé un accès aux données et une interface graphique en mode console. 

Aujourd'hui, nous allons développer une API minimale pour notre application de Todos. 
Cela nous permettra d'effectuer des opérations CRUD sur les Todos via HTTP. De plus, nous créerons un site web très simple pour interagir avec notre API.

Je vous encourage à vous inspirer de mon code pour ajouter les fonctionnalités manquantes telles que `add` et `add-subtask`.

De plus, ne consacrez pas trop de temps à cette tâche, mais assurez-vous de lire attentivement le fichier `.css` et de le modifier pour personnaliser votre interface utilisateur.

L'aspect le plus intéressant de cet exercice est de construire la solution depuis le début. Vous pouvez commencer par la commande `dotnet new web` et ensuite ajouter des références à vos dossiers `Shared` et `Data`, comme vu lors de notre dernière session.

Après avoir défini quelques endpoints, testez votre serveur avec des outils tels que Postman, curl, ou même votre navigateur. 

Ensuite, une fois que tout est fonctionnel, créez un dossier séparé contenant les fichiers `index.html`, `style.css`, et `script.js`.

Lancez votre serveur puis allez dans le susdit dossier et double-clique sur index.html.

Vous rencontrerez un problème lorsque le JavaScript tentera de récupérer des données : le serveur les refusera. Pourquoi ? Et que pouvez-vous faire pour y remédier ? Pensez à CORS.

Enfin, prenez le dossier contenant votre site et renommez-le en `wwwroot`. Intégrez-le dans votre solution de sorte à ce qu'il soit (le dossier et ses fichiers) aussi servi par le serveur car après tout c'est son job.

Pour des instructions détaillées, consultez ces ressources :
- [Static files in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/static-files?view=aspnetcore-8.0)
- [ASP.NET Core fundamentals](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/?view=aspnetcore-8.0&tabs=macos)
- [Overview of minimal APIs in ASP.NET Core](https://learn.microsoft.com/en-us/aspnet/core/fundamentals/minimal-apis/overview?view=aspnetcore-8.0)
