This is warehouse program made using .Net WindowsPresentationForms and EntityFramework with SqlLite which can keep track of products amount while we sell or resupply them.

1.Start view

We have 5 buttons 4 to functions and 1 to exit program.
![1](https://user-images.githubusercontent.com/91193062/162590658-8bab6694-9181-4b17-ad2b-62fffd3ffb28.PNG)


2.Product database view

This is where we can see all product database records ofc first we need to create one.
![2](https://user-images.githubusercontent.com/91193062/162590655-ac308363-45e8-4dfd-9d1d-ee863bce65fe.PNG)
![3](https://user-images.githubusercontent.com/91193062/162590698-f0df9ad2-7c5f-46e7-be50-bbf64b060909.PNG)

3.Supply/Sell view

Here we can increase amount of products manually or with barcode scanner.
To do that just type barcode of chosen product to textbox and hit Enter key.
It will increase amount of that product by one. You can scan products one by one or just manually overwrite product amount.
To save it to database hit "Zapisz dane" button. Selling menu works the same.

Supply
![5](https://user-images.githubusercontent.com/91193062/162591100-7440b4de-bfd5-49ce-b091-0a606123ca48.PNG)
Sell
![7](https://user-images.githubusercontent.com/91193062/162591158-7a8da1a9-25ba-4cdc-93b4-1db96a316b6c.PNG)

4.Transaction history view

Finally u can check all previous transactions.
In the left scrollviewer we can see all transactions that we made.
![9](https://user-images.githubusercontent.com/91193062/162591210-15239a7e-5c81-444f-bb52-e33e30f0ea97.PNG)
If we want to see products amount change in individual transaction just double click it s date textbox.
We can see transactions details in second scrollviewer.

Supply
![10](https://user-images.githubusercontent.com/91193062/162591276-1382bde9-d928-46e1-8154-41e176705253.PNG)
Sell
![11](https://user-images.githubusercontent.com/91193062/162591284-ca7f0997-da66-4465-8faa-56c931a0ba80.PNG)

