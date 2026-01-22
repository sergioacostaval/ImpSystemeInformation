# Cahier des charges (SRS léger) — <Stocks Investhink>
**Équipe :** <Seydina Mouhammad Sylla / Chatib Ismail / Sergio Acosta>  
**Date :** <2026-01-18>  
**Version :** <v1.0>

---

## 1. Contexte & objectif
- **Contexte :** <Pourquoi ce projet existe?>
Ce projet est né de notre passion et intérêt pour le trading et l’analyse des marchés financiers. 
Beaucoup de personnes débutantes s’intéressent au Trading, mais trouvent les outils existants trop complexes ou difficiles à comprendre. 
Cette application vise à proposer une approche plus simple et accessible.
- **Objectif principal :** <Valeur attendue / problème résolu>
L’objectif principal est de fournir une information claire et facile à comprendre à partir des prix historiques des actions, 
afin d’aider les débutants à s’intéresser au trading et à prendre des décisions de manière plus informée, 
grâce à des indicateurs simples et des signaux indicatifs d’achat ou de vente.
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
  - **Besoins :** Comprendre facilement les tendances du marché et les signaux d’achat ou de vente
  - **Contraintes :** Peu de connaissances en trading et en analyse technique
- **Acteur B : Développeur / Administrateur**
  - **Rôle :** Développer, configurer et maintenir l’application (Cycle de vie SDLC)
  - **Besoins :** Tester toutes les fonctionnalités et améliorer les règles de génération des signaux

---

## 4. Exigences fonctionnelles (FR)
- **FR-1 :** Le système doit permettre d’obtenir des données historiques de prix d’actions à partir d’une source locale (.CSV) ou d’une source web fiable (point à vérifier).
- **FR-2 :** Le système doit analyser l’évolution des prix des actions dans le temps.
- **FR-3 :** Le système doit calculer des indicateurs techniques simples (Initialement on propose SMA, EMA et RSI, mais c'est un point à vérifier).
- **FR-4 :** Le système doit générer des signaux indicatifs basés sur l’analyse technique, afin d’illustrer des situations potentielles d’achat ou de vente.
- **FR-5 :** Le système doit afficher les prix, les indicateurs et les signaux sous forme de graphiques simples.
- **FR-6 :** Le système doit produire un rapport récapitulatif des résultats de l’analyse.

---

## 5. Exigences non fonctionnelles (NFR)
> Performance / sécurité / disponibilité / UX / maintenabilité…
- **NFR-1 (Performance) :** Le système doit analyser un fichier CSV standard en moins de 2 secondes sur un ordinateur personnel.
- **NFR-2 (Sécurité) :** Le système ne doit pas stocker de données personnelles des utilisateurs.
- **NFR-3 (Disponibilité) :** L’application doit fonctionner correctement tant que l’utilisateur a une connexion internet et les données sont disponibles.
- **NFR-4 (UX) :** L’interface doit être simple et compréhensible pour un utilisateur débutant.
- **NFR-5 (Maintenabilité) :** Le code doit être propre, structuré de manière claire et bien documentée pour faciliter la maintenance et l’évolution du projet.

---

## 6. Contraintes
- **C-1 (Technologie) :** Le projet doit être développé en C# avec ASP.NET Core (MVC) en utilisant Visual Studio / Visual Studio Code.  
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
Nom : string
Email : string
Préférences : liste (tickers suivis, indicateurs favoris)
Description : Personne qui utilise l’application pour analyser les actions.

- **Règles métier :** <validation, calculs, permissions, etc.>
- Les signaux générés sont indicatifs uniquement et ne constituent pas un conseil financier.
- Les indicateurs techniques doivent être calculés à partir des prix historiques valides.
- Les graphiques et rapports doivent correspondre aux données importées et aux indicateurs calculés.
- L’utilisateur peut importer plusieurs tickers et analyser leurs prix dans le temps.
- Les données historiques doivent provenir uniquement de sources fiables ou fichiers CSV valides.
- ¿D'autres?


## 8. Hypothèses & dépendances
### 8.1 Hypothèses
- H-1 : <ex. utilisateurs ont un compte>
- H-2 : <...>

### 8.2 Dépendances
- D-1 : <API externe / BD / service>
- D-2 : <...>

---

## 9. Critères d’acceptation globaux (Definition of Done – mini)
- [ ] Fonctionnalités livrées et testées
- [ ] Tests unitaires présents
- [ ] Gestion d’erreurs minimale
- [ ] Documentation à jour (UML + ADR si requis)
