# Projet: Stocks Investhink
**Équipe :** Seydina Mouhammad Sylla / Chatib Ismail / Sergio Acosta  
**Date :** 2026-02-20  
**Version :** v1.0

--------------------------------------------------------------------------------------------------------------------------------------------
# Phase II — Analyse, exigences et premiers patrons (26 janvier au 22 février 2026)
--------------------------------------------------------------------------------------------------------------------------------------------

## Accèss au Tableau
https://trello.com/invite/b/69889d203acd92b9d2cb6acf/ATTI13fb0f74260717b76f93e96c900fda9a503A9DF0/stocks-investhink

-----------------------

## Résumé des corrections effectuées suite aux commentaires sur le SRS (Version SRS mise à jour : v2.0)
Suite aux commentaires reçus, nous avons :

1. Clarification du contexte et de l’objectif
- Clarifié que l’application a une vocation éducative et informative uniquement, et ne fournit pas de conseils financiers.
- Ajouté explicitement le système d’authentification comme partie intégrante de l’objectif.

2. Amélioration du Scope
- Précisé les limites du projet, notamment :
    * Pas de prédiction de prix
    * Pas d’intégration avec une plateforme réelle de trading
    * Pas de gestion de portefeuilles complexes
- Clarifié que seuls les stocks (actions) sont concernés.

3. Raffinement des exigences fonctionnelles (FR)
- Les FR ont été améliorées afin d’être plus détaillées, plus testables, mieux structurées

Principales améliorations :
**FR-1 :** Précision du format attendu du fichier CSV (Date/Open/High/Low/Close/Volume).
**FR-3 :** Ajout des périodes par défaut pour les indicateurs (SMA 20, EMA 20, RSI 14).
**FR-4 :** Clarification que les signaux sont indicatifs et non des recommandations financières.
**FR-7 :** Ajout des règles précises de validation du mot de passe (10 caractères minimum, majuscule, minuscule, chiffre).
**FR-8 :** Ajout de la récupération de mot de passe.
**FR-10 :** Ajout explicite de la gestion adéquate des erreurs.

4. Ajout et structuration des exigences non fonctionnelles (NFR)
- Les FR ont été améliorées afin d’être plus claires:

Principales améliorations :
**NFR-3 (Disponibilité) :** On a fait l'acclaration que si l'utilisateur travaille seulement avec le CSV, ce n'est pas neccesaire une connexion internet.
**NFR-5 (Maintenabilité) :** On ajoute OOP, patrons de conception, structure en couches.

5. Structuration des données et règles métier
- Les règles métier ont été clarifiées, notamment :
    * Les signaux sont éducatifs uniquement.
    * Les mots de passe ne sont jamais stockés en clair.
    * Chaque utilisateur accède uniquement à ses propres données.
    * Les données doivent provenir de sources fiables.

6. Modification Definition of Done
Nous avons modifié des critères d’acceptation globaux incluant :
- Implémentation complète des fonctionnalités
- Gestion des erreurs
- Tests minimaux
- Système d’authentification sécurisé

**Conclusion**
Suite aux commentaires reçus, le SRS a été rendu plus précis pour être plus cohérente, mieux définie et alignée avec les exigences académiques du projet.

-----------------------

**Référence de nos diagrammes**

**Diagramme Composants**
    Ref: ImpSystemeInformation\documentation\Diagrams\Diagramme Composants.drawio
    Explication: C'est le diagramme qui représente les composants logiciels de notre projet et les dépendances entre eux.

**Diagramme Entite-Relation**
    Ref: ImpSystemeInformation\documentation\Diagrams\Diagramme Entite-Relation.drawio
    Explication: C'est le diagramme qui modéliser la structure logique de notre projet.

**Diagramme Cas dUtilisation**
    Ref: ImpSystemeInformation\documentation\Diagrams\Diagramme Cas dUtilisation.drawio
    Explication: C'est le diagramme au haut niveau qui offre une vision globale du comportement fonctionnel de notre Projet.

**Diagramme de classes**
    Ref: ImpSystemeInformation\documentation\Diagrams\Diagrammeclasse.drawio
    Explication: C'est le diagramme qui represente la structure de notre projet et les relations qui les unissent.

--------------------------------------------------------------------------------------------------------------------------------------------
# Phase III — Raffinement architectural et conception avancée (23 février au 29 mars 2026)
--------------------------------------------------------------------------------------------------------------------------------------------

## 1. Résumé des améliorations Phase III
Suite aux commentaires reçus lors de la phase II, les suivants améliorations ont été apportées.

### Amélioration de la logique métier
Une couche de services a été ajoutée pour mieux organiser la logique métier.  
Le traitement des fichiers CSV, les calculs des indicateurs financiers et la génération des signaux sont maintenant séparés. 
Cela permet une meilleure organisation du code, une séparation claire des responsabilités et une meilleure maintenabilité.

### Raffinement de l’architecture
Le projet suit maintenant une architecture en couches :
- Controllers : gestion des requêtes  
- Services : logique métier  
- Models / ViewModels : données  
- Views : interface utilisateur  

Un Facade a également été ajouté pour simplifier l’accès aux fonctionnalités principales.

### Utilisation de patrons
Le projet utilise maintenant :
- Patron Singleton pour instancier la base de données
- Patron Commande pour executer individuellement les taches liés aux analyses (vérifier s'il existe information, nettoyer s'il faut,
importer le CSV, calculer les indicateurs, generer les signaux, graphiquer)  
- Patron Facade pour centraliser les services 


### Amélioration de l’interface utilisateur
L’interface a été améliorée avec CSS + Bootstrap dans tous les views :
- page Home structurée 
- pages Login et Register 
- page Import CSV améliorée  
- page Results avec graphiques et signaux

### Alignement avec le SRS
Les fonctionnalités principales sont maintenant implémentées :

- authentification des utilisateurs
- import CSV  
- calcul des indicateurs  
- génération des signaux  
- graphiques   

Le projet est maintenant aligné avec le SRS.

---

## 2. User Stories

On a ajoute les user stories suivants pertinentes dans Trello pour faire la gestion correspondant:

# US1 - Inscription utilisateur
En tant qu’utilisateur, je veux créer un compte avec un email et un mot de passe, afin d’accéder de manière sécurisée aux fonctionnalités de l’application.

Critères d’acceptation :
- L’utilisateur peut créer un compte avec un email valide.
- L’email doit être unique.
- Si l’email existe déjà, un message “Email déjà utilisé” est affiché.
- Le mot de passe est stocké de manière sécurisée (haché).
- Un message confirme la création du compte.
- En cas d’erreur, un message clair est affiché.
- Après inscription, l’utilisateur est redirigé vers la page Login (ou connecté automatiquement).

Definition of Done (DoD):
- Fonctionnalité implémentée.
- Aucun bug bloquant.
- Testée manuellement (Tester : succès / email déjà utilisé / champs invalides)
- Tests unitaires de base ajoutés.
- Code poussé sur GitHub

# US2 – Connexion et déconnexion
En tant qu’utilisateur, je veux me connecter et me déconnecter pour sécuriser mon compte.

Critères d’acceptation :
- Session valide après login
- Logout fonctionne
- Accès aux fonctionnalités limité pour les non-connectés

Definition of Done (DoD):
- Fonctionnalité implémentée.
- Aucun bug bloquant.
- Testée manuellement
- Code poussé sur GitHub

# US3 – Importer fichier CSV
En tant qu’utilisateur connecté, je veux importer un fichier CSV pour analyser les données.

Critères d’acceptation :
- Fichier valide
- Erreurs détectées et affichées
- Confirmation après import

Definition of Done (DoD):
- Fonctionnalité implémentée.
- Aucun bug bloquant.
- Testée manuellement
- Code poussé sur GitHub

# US4 – Calculer indicateurs techniques
En tant qu’utilisateur, je veux calculer SMA, EMA et RSI pour voir les tendances.

Critères d’acceptation :
- Calcul correct du SMA, EMA, RSI standard
- Temps < 2 secondes pour CSV standard

Definition of Done (DoD):
- Fonctionnalité implémentée.
- Aucun bug bloquant.
- Testée manuellement
- Tests unitaires de base pour le SMA.
- Code poussé sur GitHub

# US5 – Générer signaux indicatifs
En tant qu’utilisateur, je veux recevoir des signaux indicatifs d’achat/vente pour illustrer les tendances.

Critères d’acceptation :
- Signaux corrects selon les indicateurs
- Signaux associés au bon indicateur

Definition of Done (DoD):
- Fonctionnalité implémentée.
- Aucun bug bloquant.
- Testée manuellement
- Code poussé sur GitHub

# US6 – Graphiques
En tant qu’utilisateur, je veux voir des graphiques pour interpréter facilement les données.

Critères d’acceptation :
- Graphiques clairs et lisibles
- Graphique individuelle pour chaque indicateur
- Légende et couleurs correctes

Definition of Done (DoD):
- Fonctionnalité implémentée.
- Aucun bug bloquant.
- Testée manuellement
- Code poussé sur GitHub

# US7 – Rapports récapitulatifs
En tant qu’utilisateur, je veux générer un rapport récapitulatif pour revoir mes analyses.

Critères d’acceptation :
- Rapport PDF ou HTML
- Inclut prix, indicateurs et signaux

Definition of Done (DoD):
- Fonctionnalité implémentée.
- Aucun bug bloquant.
- Testée manuellement
- Code poussé sur GitHub

---

## 3. Architecture Decision Records (ADR)

### ADR-001 : Architecture MVC

Contexte  
Le projet exige une architecture claire et évolutive, une logique métier bien définie et une séparation nette des responsabilités du code.

Décision  
Utilisation de ASP.NET MVC avec une couche de services.

Justification  
- séparation claire  
- meilleure maintenance  
- code plus propre et lisible

Conséquences  
- meilleure structure general de l'application
- logique métier isolée  

---

### ADR-002 : Choix des indicateurs financiers

Contexte  
Le projet doit rester simple pour les utilisateurs.

Décision  
Utilisation de SMA, EMA et RSI.

Justification  
- indicateurs plus connus  
- faciles à comprendre
- adaptés à un usage éducatif  

Conséquences  
- système simple
- possibilité d’ajouter d’autres indicateurs  

---

## 4. Mise à jour des diagrammes UML

Les diagrammes ont été mis à jour afin de refléter l’architecture actuelle.

Le diagramme de classes inclut maintenant :
- les services (IndicatorService, SignalService, etc.)  
- les méthodes principales  
- une meilleure séparation des responsabilités