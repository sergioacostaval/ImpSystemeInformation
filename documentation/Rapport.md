# Phase II — <Stocks Investhink>
**Équipe :** Seydina Mouhammad Sylla / Chatib Ismail / Sergio Acosta  
**Date :** 2026-02-20  
**Version :** v1.0

-----------------------

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
