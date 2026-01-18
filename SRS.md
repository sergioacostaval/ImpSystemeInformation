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
grâce à des indicateurs simples et des signaux d’achat ou de vente.

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
- IN-4 : Générer des signaux d’achat ou de vente
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
- **Acteur A :** <rôle, besoins, contraintes>
- **Acteur B :** <...>

---

## 4. Exigences fonctionnelles (FR)
> Forme recommandée : “Le système doit…”
- **FR-1 :** Le système doit <...>
- **FR-2 :** Le système doit <...>

---

## 5. Exigences non fonctionnelles (NFR)
> Performance / sécurité / disponibilité / UX / maintenabilité…
- **NFR-1 (Performance) :** <ex. temps de réponse < 2s>
- **NFR-2 (Sécurité) :** <ex. authentification requise>
- **NFR-3 (UX) :** <ex. parcours en ≤ 3 clics>
- **NFR-4 (Qualité) :** <ex. couverture minimale de tests>

---

## 6. Contraintes
- **C-1 (Technologie) :** <langage / framework imposé>
- **C-2 (Plateforme) :** <web / mobile / desktop>
- **C-3 (Délai) :** <dates de phases>
- **C-4 (Outils) :** <Git, CI, etc.>

---

## 7. Données & règles métier (si applicable)
- **Entités principales :** <User, Order, ...>
- **Règles métier :** <validation, calculs, permissions, etc.>

---

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
