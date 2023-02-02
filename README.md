# PhysioReal-2767-
Dépôt PhysioReal pour la valorisation Open Innovation

Fichier Game : 
	Dans ce fichier nous retrouvons notre code C# (codé sur Unity) pour faire apparaitre les notes de musiques (ou plus précisément les boules de couleurs qui approchent du patient.

Fichier Interface :
	Dans ce fichier nous retrouvons les codes C# de gestion du curseur et d'affichage des graphiques et des valeurs obtenues après la réalisation d'un exercice.
	Les valeurs ne sont pas traitées directement à ce niveau ci. Les données passent d'abord par un script python qui va ressortir les données sous forme de 		graphique (vois les codes dans /Python/)

Fichier LinkInterface-Game : 
	Dans ce fichier nous retrouvons l'écran de paramètres et le scirpt de chargement sur la partie des paramètres choisis

Fichier Python : 
	Dans ce fichier nous retrouvons les codes Python utilisés pour traiter les résultats. Ainsi les données arrivent sous format JSON, sont traitées puis ressortent 	 sous forme de graphique. Un JSON est créé avec les valeurs suivantes : minimum, maximum et moyenne (pour BPM et Amplitude)
