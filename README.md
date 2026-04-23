# StocksInvesthink

StocksInvesthink est une application web pour analyser des actions avec des donnees historiques. Le projet permet a un utilisateur de creer un compte, de se connecter, d'importer un fichier CSV avec des prix historiques des Stocks et de voir des resultats d'analyse avec des indicateurs financiers.

Le projet utilise ASP.NET Core MVC, Entity Framework Core et une base de donnees SQLite.

## Objectif du projet

L'objectif principal est d'aider l'utilisateur a mieux comprendre l'evolution d'une action. Apres l'importation d'un fichier CSV, l'application calcule des indicateurs comme SMA, EMA et RSI. Ensuite, elle affiche des signaux indicatifs possibles, par exemple Buy, Sell ou Hold.

Ce projet est fait dans un contexte academique. Il montre aussi l'utilisation de POO et bonnes pratiques de conception, comme les services et les patrons de conception pour organiser la logique de l'analyse.

## Fonctionnalites

- Creation d'un compte utilisateur.
- Connexion et deconnexion avec authentification par cookies.
- Importation de fichiers CSV avec des prix historiques.
- Selection d'une action avant l'analyse.
- Calcul des indicateurs SMA, EMA et RSI.
- Generation de signaux indicatifs d'achat, de vente ou de conservation.
- Affichage des resultats dans une page web.
- Stockage des donnees dans SQLite.
- Tests unitaires avec NUnit.

## Technologies utilisees

- C#
- ASP.NET Core MVC
- .NET
- Entity Framework Core
- SQLite
- HTML, CSS, Bootstrap, JS et Razor Views
- NUnit pour les tests

## Structure du projet

```text
StocksInvesthink/
|
+-- StocksInvesthink.sln
|
+-- StocksInvesthink/
|   +-- Controllers/
|   +-- Data/
|   +-- Migrations/
|   +-- Models/
|   +-- Services/
|   +-- ViewModels/
|   +-- Views/
|   +-- wwwroot/
|   +-- Program.cs
|   +-- StocksInvesthink.csproj
|   +-- stocksinvesthink.db
|
+-- NUTest/
|   +-- NUTest.csproj
|   +-- UnitTest1.cs
|
+-- Documentation/
    +-- README.md
    +-- Rapport_Final.md
    +-- SRS.md
    +-- StocksInvesthink_Phase4.pptx
    +-- UML/
    +-- Screenshots/
    +-- fichiers CSV pour les tests/
```

## Documentation incluse

Le dossier `Documentation` contient les documents et les fichiers necessaires pour comprendre et tester le projet.

- Les fichiers `.csv` sont utilises pour faire des tests dans l'application.
- `Rapport_Final.md` Contient toute la description de projet final, même les explications concernant les choix structural, la technologie utilisée, etc.
- `SRS.md` contient les exigences du systeme qu'on a defini avant de commencer.
- `StocksInvesthink_Phase4.pptx` est la presentation PowerPoint du projet.
- Le dossier `UML` contient tous les diagrammes UML du systeme.
- Le dossier `Screenshots` contient les captures d'ecran de l'execution.

## Prerequis

Avant de lancer le projet, il faut installer :

- Visual Studio 2022 ou une version compatible.
- Le SDK .NET 10.0.
- Les outils Entity Framework Core, si vous voulez modifier ou recreer la base de donnees.

## Execution du projet
1. Cloner ou telecharger le projet.
2. Ouvrir un terminal dans le dossier principal du projet.
3. Compiler la solution
4. Lancer l'application

## Utilisation de l'application

1. Ouvrir l'application dans le navigateur.
2. Creer un compte avec un mot de passe valide.
3. Se connecter avec l'email et le mot de passe.
4. Aller a la page d'importation CSV.
5. Choisir une Action (Stock) dans la liste.
6. Importer un fichier `.csv` depuis le dossier `Documentation`.
7. Voir les resultats de l'analyse.

Le fichier CSV doit contenir des donnees historiques avec les colonnes attendues par l'application. Le format utilise est base sur les fichiers de prix historiques de Yahoo Finance.

## Tests

Le projet contient un projet de tests appele `NUTest`.

Pour executer les tests on peut utiliser le "test explorer". On peut testet la création d'un utilisateur, le Hachage et la connexion.

## Base de donnees

L'application utilise SQLite avec le fichier :

```text
StocksInvesthink/stocksinvesthink.db
```

Les migrations Entity Framework Core sont dans le dossier :

```text
StocksInvesthink/Migrations
```

Si la base de donnees doit etre recreee, il faut utiliser les migrations du projet.

## Notes importantes

- Les fichiers CSV de test ne sont pas dans le code source principal. Ils doivent etre dans le dossier `Documentation`.
- Les resultats dependent des donnees importees.
- Les captures d'ecran dans `Documentation/Screenshots` montrent des exemples d'execution.
- Les diagrammes dans `Documentation/UML` aident a comprendre la structure du systeme.

## Auteurs

Projet developpe pour le cours d'Implementation des Systemes d'Information par:
Sergio Acosta
Seydina Mouhammad Sylla
Chatib Ismail