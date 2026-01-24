# Cahier des charges (SRS léger) — <Stocks Investhink>
**Équipe :** Seydina Mouhammad Sylla / Chatib Ismail / Sergio Acosta  
**Date :** 2026-01-18  
**Version :** v1.0

---

## 1. Contexte & objectif
- **Contexte :** <Pourquoi ce projet existe?>
Ce projet est né de notre passion et intérêt pour le trading et l’analyse des marchés financiers. 
Beaucoup de personnes débutantes s’intéressent au Trading, mais trouvent les outils existants trop complexes ou difficiles à comprendre. 
Cette application vise à proposer une approche plus simple et accessible.
- **Objectif principal :** <Valeur attendue / problème résolu>
L’objectif principal est de fournir une information claire et facile à comprendre à partir des prix historiques des actions, 
afin d’aider les débutants à s’intéresser au trading et à prendre des décisions de manière plus informée, 
grâce à des indicateurs simples et des signaux indicatifs d’achat ou de vente. L’application inclut un système d’authentification (compte utilisateur avec identifiant et mot de passe) afin de personnaliser l’expérience et sécuriser l’accès aux fonctionnalités.
- **Parties prenantes :** <utilisateurs, client, admin, etc.>
Utilisateur final : Personnes débutantes en Trading
Développeurs : Étudiants passionnés par le Trading
¿¿D'autres??

---

## 2. Portée (Scope)
### 2.1 Inclus (IN)
- IN-1 : Importer des données historiques de prix d’actions à partir de fichiers CSV  
- IN-2 : Analyser l’évolution des prix des actions dans le temps  
- IN-3 : Calculer des indicateurs techniques simples (SMA, EMA, RSI) 
- IN-4 : Générer des signaux indicatifs basés sur l’analyse technique, afin d’illustrer des situations d’achat ou de vente.
- IN-5 : Afficher les résultats sous forme de graphiques simples
- IN-6 : Produire un rapport récapitulatif des tendances et des signaux

### 2.2 Exclu (OUT)
- OUT-1 : Analyse d'autres actifs financiers que les actions (Wall Street Stocks)
- OUT-2 : Prédiction des prix futurs des actions
- OUT-3 : Conseils financiers personnalisés
- OUT-4 : Connexion à une vraie plateforme de trading pour acheter ou vendre des actions
- OUT-5 : Gestion de portefeuilles complexes ou multi-utilisateurs

---

## 3. Acteurs / profils utilisateurs
- **Acteur A : Utilisateur débutant** 
  - **Rôle :** Utiliser l’application pour analyser des actions (Stocks)
  - **Accès :** : L’utilisateur doit créer un compte et s’authentifier pour utiliser l’application.
  - **Besoins :** 
      - Comprendre facilement les tendances du marché et les signaux d’achat ou de vente
      - Accéder à ses analyses de manière sécurisée et personnalisée.
  - **Contraintes :** Peu de connaissances en trading et en analyse technique
- **Acteur B : Développeur / Administrateur**
  - **Rôle :** Développer, configurer et maintenir l’application (Cycle de vie SDLC)
  - **Besoins :** 
      - Tester toutes les fonctionnalités et améliorer les règles de génération des signaux
      - Gérer les comptes utilisateurs et assurer la sécurité de l’authentification.

---

## 4. Exigences fonctionnelles (FR)
- **FR-1 :** Le système doit permettre d’obtenir des données historiques de prix d’actions à partir d’une source locale (.CSV) ou d’une source web fiable (point à vérifier).
- **FR-2 :** Le système doit analyser l’évolution des prix des actions dans le temps.
- **FR-3 :** Le système doit calculer des indicateurs techniques simples (Initialement on propose SMA, EMA et RSI, mais c'est un point à vérifier).
- **FR-4 :** Le système doit générer des signaux indicatifs basés sur l’analyse technique, afin d’illustrer des situations potentielles d’achat ou de vente.
- **FR-5 :** Le système doit afficher les prix, les indicateurs et les signaux sous forme de graphiques simples.
- **FR-6 :** Le système doit produire un rapport récapitulatif des résultats de l’analyse.
- **FR-7 :** Le système doit permettre à un utilisateur de créer un compte avec un identifiant (email) et un mot de passe.
- **FR-8 :** Le système doit permettre à un utilisateur authentifié de se connecter et de se déconnecter.
- **FR-9 :** Le système doit restreindre l’accès aux fonctionnalités de l’application aux utilisateurs authentifiés.


---

## 5. Exigences non fonctionnelles (NFR)
> Performance / sécurité / disponibilité / UX / maintenabilité…
- **NFR-1 (Performance) :** Le système doit analyser un fichier CSV standard en moins de 2 secondes sur un ordinateur personnel.
- **NFR-2 (Sécurité) :** Le système doit assurer une gestion sécurisée des comptes utilisateurs (hachage des mots de passe) et ne doit pas stocker de données personnelles sensibles.
- **NFR-3 (Disponibilité) :** L’application doit fonctionner correctement tant que l’utilisateur a une connexion internet et les données sont disponibles.
- **NFR-4 (UX) :** L’interface doit être simple et compréhensible pour un utilisateur débutant.
- **NFR-5 (Maintenabilité) :** Le code doit être propre, structuré de manière claire et bien documentée pour faciliter la maintenance et l’évolution du projet.

---

## 6. Contraintes
- **C-1 (Technologie) :** Le projet doit être développé en C# avec ASP.NET Core (MVC) en utilisant une base de données relationnelle locale (ex. SQLite). (Ex: Visual Studio / Visual Studio Code.)
- **C-2 (Plateforme) :** L’application sera une application web accessible via navigateur, compatible avec PC et tablettes. 
- **C-3 (Délai) :**  
  - Phase I – Lancement du projet : 8 janvier au 25 Janvier 2026  
  - Phase II – Analyse, exigences et premiers patrons: 26 janvier au 22 février 2026 
  - Phase III – Raffinement architectural et conception avancée : 23 février au 22 mars 2026
  - Phase IV – Intégration, optimisation et robustesse : 23 mars au 19 Avril 2026
- **C-4 (Outils) :** 
  - Git pour le versioning du code
  - GitHub pour le dépôt
  - Draw.io pour le UML
  - Visual Studio / VS Code pour le codage
- **C-5 (Données) :** Les données historiques des stocks proviendront soit de fichiers CSV fournis, soit de sources web fiables ; aucune donnée sensible ne sera collectée.

---

## 7. Données & règles métier (si applicable)
- **Entités principales :**
Stock
Attributs :
Ticker (symbole) : string (ex. AAPL)
Nom : string (ex. Apple Inc.)
Prix historique : liste de float, avec dates correspondantes

Description : Représente un Stock sur le marché boursier avec ses données historiques.

Indicateur technique
Attributs :
Nom : string (ex. SMA, EMA, RSI)
Valeur : float
Période : entier (ex. 14 pour RSI, 50 pour SMA)
Description : Calculé à partir des prix historiques pour aider à visualiser les tendances.

Signal indicatif
Attributs :
Type : string (Achat potentiel, Vente potentielle)
Date : date du signal
Prix : float
Indicateur associé : référence à l’indicateur utilisé
Description : Représente une situation illustrative, pour montrer où un utilisateur pourrait considérer d’acheter ou vendre.

Utilisateur (User)
Attributs :
Id : entier
Nom : string
Email : string (unique)
MotDePasseHash : string
Préférences : liste (tickers suivis, indicateurs favoris)
Descripción: Représente un utilisateur authentifié de l’application. Un compte est requis pour accéder aux fonctionnalités d’analyse.

- **Règles métier :** <validation, calculs, permissions, etc.>
- Les signaux générés sont indicatifs uniquement et ne constituent pas un conseil financier.
- Les indicateurs techniques doivent être calculés à partir des prix historiques valides.
- Les graphiques et rapports doivent correspondre aux données importées et aux indicateurs calculés.
- L’utilisateur peut importer plusieurs tickers et analyser leurs prix dans le temps.
- Les données historiques doivent provenir uniquement de sources fiables ou fichiers CSV valides.
- L’utilisateur doit être authentifié pour accéder aux fonctionnalités d’analyse.
- Les mots de passe ne sont jamais stockés en clair.
- Chaque utilisateur accède uniquement à ses propres préférences et analyses.
- ¿D'autres?


## 8. Hypothèses & dépendances
### 8.1 Hypothèses
- **H-1 :** Les utilisateurs ont des connaissances de base en informatique et savent utiliser un navigateur web.
- **H-2 :** Les utilisateurs disposent d’une connexion internet pour accéder aux données boursières.
- **H-3 :** Les indicateurs techniques simples (telles que SMA, EMA, RSI) sont suffisants pour une première approche du trading.
- **H-4 :** L’application est utilisée uniquement à des fins éducatives et informatives.
- **H-5 :** Les utilisateurs acceptent de créer un compte afin d’accéder aux fonctionnalités de l’application.


### 8.2 Dépendances
- **D-1 :** Le fonctionnement de l’application dépend de la disponibilité des sources de données boursières (fichiers CSV ou sources web fiables).
- **D-2 :** Le système dépend du framework ASP.NET Core pour le traitement et l’affichage des données.
- **D-3 :** L’affichage des graphiques dépend de bibliothèques de visualisation web.
- **D-4 :** Le système dépend d’une base de données locale pour la gestion des comptes utilisateurs.


---

## 9. Critères d’acceptation globaux (Definition of Done – mini)
- [ ] Toutes les fonctionnalités décrites sont implémentées, fonctionnelles et sont couvertes par des tests unitaires.
- [ ] Les erreurs courantes (fichiers invalides, données manquantes, erreurs de calcul) sont gérées correctement.
- [ ] L’interface utilisateur permet une utilisation simple et compréhensible pour un utilisateur débutant.
- [ ] La documentation technique et fonctionnelle est à jour (UML, description des choix techniques).
- [ ] Le code source et disponible sur le dépôt GitHub du projet.
- [ ] Le système d’authentification (inscription, connexion, déconnexion) est fonctionnel et sécurisé.
