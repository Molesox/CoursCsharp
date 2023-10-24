Voici quelques exercices pour s'entraîner à l'héritage & cie. Pour optimiser votre expérience d'apprentissage, j'ai pris soin de mettre en **gras** certaines définitions ou points subtiles de l'énoncé et j'ai intégré des liens pertinents qui approfondissent les informations présentées. Je vous encourage vivement à explorer ces ressources, car elles constituent un instrument indispensable pour votre progression et vos futurs développements dans ce domaine.

## La base `Vehicule.cs`

Dans cet exo, vous allez créer une classe de **base** représentant un véhicule. Cette classe intégrera des informations générales pertinentes pour tous les types de véhicules.

**Définition de la Classe :**
- Créez un fichier nommé `Vehicule.cs`.
- Dans ce fichier, définissez une classe publique `Vehicule` qui inclut les attributs suivants :
    - `Marque` pour la marque du véhicule.
    - `DateAchat` pour la date d'achat du véhicule.
    - `PrixDAchat` pour le prix d'achat du véhicule.
    - `PrixCourant` pour le prix courant du véhicule (attribut calculé).
    - `Age` pour l'âge du véhicule (attribut calculé).

**Constructeur :**
    - Ajoutez un constructeur public à la classe `Vehicule`. Ce constructeur doit accepter trois paramètres : `Marque`, `DateAchat`, et `PrixDachat`, correspondant aux attributs ci-dessus.
    - Le constructeur doit initialiser les attributs de l'instance avec les valeurs fournies. 

 
**Surcharge de Méthode :**
Vous allez **surcharger** la méthode `ToString()` qui est héritée de la classe de base `Object` de .NET [(plus d'infos ici ! )](https://learn.microsoft.com/en-us/dotnet/api/system.object?view=net-7.0). 
Cette surcharge permet d'offrir un moyen personnalisé de représenter une instance de `Vehicule` sous forme de chaîne de caractères.

```csharp
public override string ToString()
```
        
Dans cette méthode, retournez une chaîne de caractères qui représente l'état de l'objet `Vehicule` (les valeurs de ses attributs).


---
## Les classes enfant

Maintenant, vous allez **étendre** la classe `Vehicule` pour créer des classes spécialisées représentant différents types de véhicules : une `Voiture` et un `Avion`. Ces nouvelles classes incluront des attributs spécifiques à chaque type.

### Classe `Voiture.cs`

**Définition de la Classe :**
- Créez un nouveau fichier nommé `Voiture.cs`.
- Définissez une [classe](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/class) publique `Voiture` qui hérite de la classe `Vehicule`.
- Ajoutez les attributs suivants à la classe `Voiture` :
    - `Cylindree` pour la cylindrée de la voiture.
    - `NombreDePortes` pour le nombre de portes de la voiture.
    - `Puissance` pour la puissance de la voiture.
    - `Kilometrage` pour le kilométrage de la voiture.

**[Constructeur](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/constructors) :**
    - Ajoutez un constructeur public à la classe `Voiture`. Ce constructeur doit accepter les paramètres nécessaires pour initialiser **tous** les attributs.
    - Utilisez l'instruction [`base`](https://learn.microsoft.com/en-us/dotnet/csharp/language-reference/keywords/base) pour appeler le [constructeur](https://learn.microsoft.com/en-us/dotnet/csharp/programming-guide/classes-and-structs/using-constructors) (pas le même lien) de la classe **parente** `Vehicule`.

**ToString() :**
    - Modifier le code pour que la méthode ``ToString()`` affiche non seulement les informations de base du véhicule, mais aussi les détails spécifiques à la `Voiture`.

### Classe `Avion.cs`

**Définition de la Classe :**
- Créez un nouveau fichier nommé `Avion.cs`.
- Définissez la **sous-classe** publique `Avion` qui hérite de la classe `Vehicule`.
- Ajoutez les attributs suivants à la classe `Avion` :
    - `Type` pour le type d'avion (hélices ou réaction).
    - `HeuresDeVol` pour le nombre d'heures de vol de l'avion.

**Constructeur :**
    - Ajoutez un constructeur public à la classe `Avion`. Ce constructeur doit accepter les paramètres nécessaires pour initialiser **tous** les attributs.
    - Utilisez l'instruction `base` pour appeler le constructeur de la classe parente `Vehicule`.

**ToString() :**
   - Modifier le code pour que la méthode ``ToString()`` affiche non seulement les informations de base du véhicule, mais aussi les détails spécifiques à la `Avion`.

---

## Classe Véhicule le retour

Ajoutez une méthode ``CalculerPrix()`` dans la classe `Vehicule`.

```csharp
public void CalculerPrix(){}
```

Cette méthode fixera le ``Prix Courant`` de la manière suivante : elle déduira 1% du prix d'achat pour chaque année écoulée entre la date d'achat et l'année courante. Pour l'année courante, utilisez [DateTime](https://learn.microsoft.com/en-us/dotnet/api/system.datetime.date?view=net-7.0).

## Sous-classes Voiture et Avion

Redéfinissez la méthode `calculePrix()` pour ces deux sous-classes, en prenant en compte les critères spécifiques à chaque type de véhicule pour calculer le prix courant. Après le calcul, mettez à jour l'attribut correspondant au prix courant.

```csharp
public void CalculerPrix(){}
```

Le prix courant est calculé à partir du prix d'achat initial en appliquant les déductions suivantes :
### Voiture :
  - 2% pour chaque année depuis la date de fabrication jusqu'à l'année courante.
  - 5% pour chaque tranche de 10 000 km parcourus (arrondi à la tranche de 10 000 km la plus proche).
  - 10% de réduction si le véhicule est de marque "Renault" ou "Fiat" .
  - Un **ajout** de 20% au prix si le véhicule est de marque "Ferrari" ou "Porsche" .
### Avion :  
  - 10% pour chaque tranche de 1 000 heures de vol pour les avions à réaction.
  - 10% pour chaque tranche de 100 heures de vol pour les avions à hélices.

**Note Importante :** Si, après tous les calculs, le prix courant s'avère être négatif, il doit être ajusté à 0 pour s'assurer que le prix reste positif.

---
## Exemple de main

```csharp
List<Voiture> garage = new List<Voiture>
{
    new Voiture("Peugeot", new DateTime(1998, 01, 01), 147325.79m, 2.5, 5, 180.0, 12000),
    new Voiture("Renault", new DateTime(2005, 06, 18), 20000.00m, 1.9, 4, 130.0, 23000),
    new Voiture("Porsche", new DateTime(2012, 03, 29), 25000.00m, 2.2, 5, 150.0, 17500),
};

List<Avion> hangar = new List<Avion>
{
    new Avion("Boeing", new DateTime(2008, 11, 10), 150000, AvionType.Reaction, 1200.5m),
    new Avion("Sesna", new DateTime(1972, 1, 1), 1230673.90m, AvionType.Helice, 250),
};

Console.WriteLine("Voitures:");
foreach (var voiture in garage)
{
    voiture.CalculerPrix();
    Console.WriteLine(voiture);
}

Console.WriteLine("\nAvions:");
foreach (var avion in hangar)
{
    avion.CalculerPrix();
    Console.WriteLine(avion);
} 
```

En l'état actuel, le résultat devrait ressembler à ce qui suit. 
### Voitures

```
-----------------------------
Marque: Peugeot
Date d'achat: 01.01.1998
Âge du véhicule: 25 années
Prix d'achat: 147'325,79 CHF
Prix courant: 64'823,35 CHF
Cylindrée: 2.5 cm3
Nombre de portes: 5
Puissance: 180 chevaux
Kilométrage: 12'000.0 km
-----------------------------
Marque: Renault
Date d'achat: 18.06.2005
Âge du véhicule: 18 années
Prix d'achat: 20'000,00 CHF
Prix courant: 12'500,00 CHF
Cylindrée: 1.9 cm3
Nombre de portes: 4
Puissance: 130 chevaux
Kilométrage: 23'000.0 km
-----------------------------
Marque: Porsche
Date d'achat: 29.03.2012
Âge du véhicule: 11 années
Prix d'achat: 25'000,00 CHF
Prix courant: 12'312,50 CHF
Cylindrée: 2.2 cm3
Nombre de portes: 5
Puissance: 150 chevaux
Kilométrage: 17'500.0 km
-----------------------------
```

### Avions

```
-----------------------------
Marque: Boeing
Date d'achat: 10.11.2008
Âge du véhicule: 15 années
Prix d'achat: 150'000,00 CHF
Prix courant: 131'992,50 CHF
Type d'avion: Reaction
Heures de vol: 1'200.5
-----------------------------
Marque: Sesna
Date d'achat: 01.01.1972
Âge du véhicule: 51 années
Prix d'achat: 1'230'673,90 CHF
Prix courant: 923'005,43 CHF
Type d'avion: Helice
Heures de vol: 250.0
-----------------------------
```

## Polymorphisme

Nous cherchons à créer une classe unifiée, `Aeroport`, qui gérera des ensembles de véhicules, notamment des voitures et des avions. Cette classe vise à remplacer les structures séparées de garage et de hangar tout en évitant la duplication de code. L'objectif est de pouvoir interagir avec la collection de véhicules de manière transparente, comme dans l'exemple suivant :

```c#
var genf = new Aeroport();
genf.AddRange(hangar);
genf.AddRange(garage);

Console.WriteLine($"Il y a {genf.Count} véhicules dans l'aeroport.");
foreach (var vehiculce in genf )
{
    vehiculce.CalculerPrix();
    Console.WriteLine(vehiculce);
}
```

Pour atteindre ce résultat, quel type d'héritage ou de structure de classe devriez-vous implémenter? (une ligne de code devrait suffire).
En exécutant le code dans l'état actuel, vous devriez observer le résultat suivant :

```
Il y a 5 véhicules dans l'aeroport.
-----------------------------
Marque: Boeing
Date d'achat: 10.11.2008
Âge du véhicule: 15 années
Prix d'achat: 150'000,00 CHF
Prix courant: 127'500,00 CHF
Type d'avion: Reaction
Heures de vol: 1'200.5
-----------------------------
Marque: Sesna
Date d'achat: 01.01.1972
Âge du véhicule: 51 années
Prix d'achat: 1'230'673,90 CHF
Prix courant: 603'030,21 CHF
Type d'avion: Helice
Heures de vol: 250.0
-----------------------------
Marque: Peugeot
Date d'achat: 01.01.1998
Âge du véhicule: 25 années
Prix d'achat: 147'325,79 CHF
Prix courant: 110'494,34 CHF
Cylindrée: 2.5 cm3
Nombre de portes: 5
Puissance: 180 chevaux
Kilométrage: 12'000.0 km
-----------------------------
Marque: Renault
Date d'achat: 18.06.2005
Âge du véhicule: 18 années
Prix d'achat: 20'000,00 CHF
Prix courant: 16'400,00 CHF
Cylindrée: 1.9 cm3
Nombre de portes: 4
Puissance: 130 chevaux
Kilométrage: 23'000.0 km
-----------------------------
Marque: Porsche
Date d'achat: 29.03.2012
Âge du véhicule: 11 années
Prix d'achat: 25'000,00 CHF
Prix courant: 22'250,00 CHF
Cylindrée: 2.2 cm3
Nombre de portes: 5
Puissance: 150 chevaux
Kilométrage: 17'500.0 km
-----------------------------
```
 
**Cependant, vous remarquerez que le "prix courant" ne reflète pas les valeurs attendues. !** 

Si vous placez des points d'arrêt dans les méthodes `CalculerPrix()` de chaque classe, vous constaterez que c'est la méthode de `Vehicule` qui est invoquée. Dans le premier ``main``, cela n'était pas le cas. Pourquoi ? Parce que dans le premier "main", nous avions affaire à une liste d'instances de `Voiture`, et donc c'était la méthode `CalculerPrix()` de la classe `Voiture` qui était appelée (il en va de même pour `Avion`).

Cependant, dans `Aeroport`, nous manipulons des instances de `Vehicule` (la classe parente). De ce fait, c'est logiquement la méthode `CalculerPrix()` de la classe `Vehicule` qui est appelée.

Malgré cela, nous souhaiterions que notre programme détermine _dynamiquement_ la méthode `CalculerPrix()` à appeler, selon qu'il s'agit d'une instance de `Voiture` ou d'`Avion`, et invoque la méthode des classes respectives.

Pour réaliser cela, nous devons modifier la signature de la méthode `CalculerPrix()` dans `Vehicule.cs` en la déclarant comme `virtuelle` :

```csharp
 public virtual void CalculerPrix(){}
```

En déclarant la méthode comme `virtual`, nous autorisons les classes dérivées à fournir leur propre implémentation de cette méthode, en la *remplaçant* / *surchargeant*.  Cela signifie que si la méthode est définie à nouveau dans une classe fille, cette nouvelle version sera appelée à la place de la méthode originale de la classe parente, en fonction du type réel de l'objet.

Ce processus est rendu possible par un mécanisme appelé "liaison tardive" ou "liaison dynamique". Contrairement à la "liaison précoce" où l'adresse de la méthode à exécuter est déterminée lors de la compilation, la liaison tardive diffère cette décision jusqu'à l'exécution du programme. Cela signifie que le code compile une sorte de "référence" ou de "pointeur" à la méthode, plutôt que de lier directement à l'implémentation spécifique de la méthode.

Ainsi, lors de l'exécution, lorsque nous appelons une méthode marquée comme `virtual` sur un objet, le système d'exécution cherche la version de la méthode qui correspond le mieux au type réel de cet objet. Si l'objet est une instance d'une classe dérivée qui a remplacé la méthode originale, alors c'est cette version qui est exécutée. C'est ce mécanisme qui est au cœur du polymorphisme en programmation orientée objet, permettant à différentes classes de fournir des comportements spécifiques et diversifiés tout en utilisant la même interface de méthode.

Il faut donc préciser aux classes fille qu'elle surchargent la méthode virtuelle comme suit:

```csharp
  public override void CalculerPrix(){}
```

## Abstraction

Dans notre modèle actuel, rien n'empêche le code de créer des instances directes de la classe `Vehicule` avec la syntaxe standard :

```csharp
var vehicule = new Vehicule();
```

Cependant, cette action n'a pas beaucoup de sens. En réalité, "Vehicule" est un concept général, une idée, plutôt qu'une entité spécifique. On n'utilise jamais un "véhicule" de manière abstraite dans la vie réelle ; on utilise toujours une instance concrète d'un certain type de véhicule, comme une voiture, un avion, etc.

Pour refléter cette réalité dans notre code, nous devrions empêcher la création d'objets de type `Vehicule` directement. Cela nous amène au concept d'abstraction en programmation orientée objet. En marquant la classe `Vehicule` comme `abstract`, nous posons une règle stricte : cette classe sert de base pour d'autres, mais ne peut pas être instanciée elle-même.

```csharp
public abstract class Vehicule
{
    //...
}
```
C'est comme le concept de "Couleur" dans le monde réel. On ne crée pas une "couleur" en soi ; on crée une instance spécifique d'une couleur, comme "Rouge" ou "Bleu". De même, "Vehicule" est une idée abstraite, et seules ses sous-classes, représentant des types de véhicules spécifiques, devraient être instanciables.
