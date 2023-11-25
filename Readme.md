  
# Cours sur C# et dotnet Syslog

Ce référentiel contient le code source des projets du cours en C#. Suivez les instructions ci-dessous pour cloner le projet et l'exécuter à l'aide de `dotnet run` ou le construire avec `dotnet build`.

## Prérequis

Vérifiez que les éléments suivants sont installés sur votre système :

- [.NET Core SDK](https://dotnet.microsoft.com/download) (version 7 ou supérieure)
- [Visual Studio Code (VS Code)](https://code.visualstudio.com/)

## Clonage du dépôt

Clonez ce dépôt sur votre machine locale en utilisant la commande suivante :

```bash
git clone https://github.com/Molesox/CoursCsharp.git
cd CoursCsharp
```

## Structure des Cours

Chaque cours a son propre dossier tel que [`Cours01`](Cours01/), [`Cours02`](Cours02/), [`Cours03`](Cours03/),  [`Cours04`](Cours04/Readme.md),...

```bash
cd Cours01
```

## Exécution du Projet

Pour exécuter le projet, utilisez la commande suivante :

```bash
dotnet run
```

Cela va construire le projet et lancer l'application.

## Construction du Projet

Pour construire le projet sans l'exécuter, utilisez la commande suivante :

```bash
dotnet build
```

Cela va compiler le code source et générer les fichiers binaires nécessaires sans exécuter l'application.

## Exécution des Tests

Pour vérifier la validité de vos implémentations, accédez au projet [`ExercisesEvaluator`](ExercisesEvaluator/) et exécutez la commande :

```bash
dotnet test
```

Cela affichera les tests réussis ou échoués.