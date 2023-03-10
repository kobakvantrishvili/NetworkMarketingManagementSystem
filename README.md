# NMMS

პროექტი დაწერილია ASP.NET Core MVC-ში რადგან ვაპირებდი მარტივი Front-ის დამატებას, რათა ფუნქციების შესრულება ვიზუალურად უფრო კარგად ყოფილიყო წარდგენილი.
დროის ლიმიტის გამო Front-ის მხარე არ დამიმატებია.

უშუალოდ პროექტს რაც შეეხება:
1.  დისტრიბუტორის, პროდუქტების და გაყიდვების მონაცემების შესანახად ვიყენებ MS SQL-ს (ბაზის სანახავად იხ. ატვირთული სურათი)
2.  Online-ში არ გამიკეთებია Database-ი შესაბამისად MSSQL-ისთვის ConnectionString-ი ხელით უნდა გაუწეროთ appsettings.json-ში და მიგრაციები გაუშვათ
3.  ბონუსების შესანახად ვიყენებ Mongo DB-ს (Online Database)
4.  ბონუსების და გაყიდვების, სორტირებისთვის და ფილტრისთვის ვიყენებ OData-ს
5.  მომხმარებლის input-ის ვალიდაციისთვის ვიყენებ Fluent Validations-ს, ვალიდაციები მიწერია Web layer-ის Infrastructure ფოლდერში
6.  mapping-ების გასაწერად ვიყენებ Mapster-ს რომლის კონფიგურაცი მიწერია Web layer-ის Infrastructure ფოლდერში
7.  Infrastructure ფოლდერში მიწერია ასევე ServiceExtension-ის ფაილი სადაც გაწერილი მაქვს მოცემული ინტერფეისისთვის რომელი იმპლემენტაცია გამოიყენოს Framework-მა
8.  Service Layer-ში (.Application) მიწერია ძირითადი ლოგიკა
9.  Persistance Layer-ში ცალ-ცალკე მაქვს MSSQL-ის და MongoDB-ის რეპოსიტორები, სადაც მონცემთა ბაზაში მონაცემების შეტანის, წამოღების და რედაქტირების ფუნქციები მაქვს.
10. Data Layer-ში უშუალოდ Table-ების ანალოგური კლასები მაქვს.

NOTE: Exception-ების throw-ის და შემდეგ Global Exception Handler-ის იმპლემენტირების მაგივრად Service Layer-იდან ვაბრუნებ Status Code-ებს

OData-ს query-ები (ფილტრისთვის, სორტირებისთვის და სელექთისთვის) შემდეგნაირად გამოიყურება:
1. თუ მინდა ისეთი sale-ების გაფილტვრა რომლებიც შეასრულა დისტრიბუტორმა სახელად "Koba" მაშინ filter გრაფაში დავწერ: distributorFirstName eq 'Koba'
2. თუ მინდა ისეთი sale-ების გაფილტვრა სადაც ერთ-ერთი გაყიდული პროდუქტია "Chocolate" მაშინ filter გრაფაში დავწერ: 'chocolate' in ProductNames
3. თუ მინდა sale-იდან მხოლოდ sale-ის Id-ს წამოღება მაშინ select გრაფაში დავწერ: Id 
4. თუ მინდა sale-ების TotalPrice-ის კლების მიხედვით დალაგება orderby გრაფაში დავწერ: TotalPrice desc
5. მსგავსად შეგვიძლია ასევე ბონუსების გაფილტვრაც : EndDate gt '2022-12-25T00:00:00' - დააბრუნებს იმ ბონუსებს რომლებიც 25 დეკემბრის შემდეგ იქნა დათვლილი
