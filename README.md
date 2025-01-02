# ExamDotJanvier2023

![alt text](image.png)

Point 0
Le but est de créer la base de données Northwind et de faire en sorte que le projet l’utilise via l’Entity Framework.
Indications utiles :
N’oubliez pas d’activer le proxy lazy loading. Pour rappel, cela se fait en installant le package Microsoft.EntityFrameworkCore.Proxies et en ajoutant UseLazyLoadingProxies() dans le context créé.

protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
=> optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=Northwind").UseLazyLoadingProxies();

Point 1
Le but est d’afficher dans une combobox la liste des produits encore proposé dans le catalogue des produits.
Indications utiles :

1. Un produit retiré du catalogue est noté “discontinued” dans la base de données
2. Voici un itemtemplate permettant l’affichage de la combobox comme présenté ci-dessus
   <DataTemplate x:Key="listTemplate">
   <StackPanel Margin="0 5 0 5">
   <Label Content="{Binding ProductId}" HorizontalAlignment="Left" VerticalAlignment="Center"/>
   <Label Content="{Binding ProductName}" HorizontalAlignment="Right" VerticalAlignment="Center"/>
   </StackPanel>
   </DataTemplate>

Point 2
Le but est simplement d’afficher le produit sélectionné dans la combobox. Remarquez qu’il n’y a que 4 champs affichés. Le champ affiché pour le fournisseur est le “ContactName”.

Point 3
Le but est de retirer un produit du catalogue des produits. Ce bouton marque simplement le produit comme abandonné (“discontinued”). Dès que l’on marque un produit comme abandonné, il disparait instantanément de la combobox.

Point 4
Le but est d’afficher le nombre de produits par pays qui ont été au moins vendu une fois. Cette liste est triée par ordre décroissant du nombre de produits. Vous avez le résultat sur l’image ci-dessus.
