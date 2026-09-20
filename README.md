1. La boucle dépend de la machine où elle tourne et les objets sont compter comme les règles du jeu donc ça reste dans Core.
2. FixedUpdate tourne toujours au même rythme. Si on utilisait Update, la puissance du PC changerai la donne sur la vitesse de déplacement, 
    un PC moins puissant serait plus lent qu'un PC puissant.
3. J'ai représenté "aucune destination sélectionnée" avec un index à -1. Ma première solution était d'utilisé 0 
    comme valeur spéciale mais c'était pas fonctionnel car 0 est aussi l'index de la première destination.
4. Une touche pressée c'est un moment précis, donc un évènement prète à l'utilisation. L'affichage, c'est un état permanent, un simple accesseur suffit. 
    Dans les deux cas : la console ne va jamais dans Core.
5. Il suffit d'ajouter un TavernComponent et un ShopComponent aux bons GameObject, 
    uniquement dans la classe qui construit le monde. Aucune autre classe ne change par la composition.
6. Seul Presentation change pour le graphique. Tout Core reste identique.
7. Commit atomique : "Fix : Retirer les Using inutile", j'ai seulement retirer les using qui ne servait à rien donc le code n'avait aucun problème pour fonctionner  
   (Le fix de correction de la boucle FixedUpdate et suppression d'un Manager inutile lors du premier cours rentre aussi dans un commit atomique")
  Commit non atomique : "Moteur : Séparation des répertoires et HotFix" fait la séparation Core/Presentation avec d'autres nouveaux fichiers.cs qui auraient dû être des commits séparés.
