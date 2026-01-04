# Casus programmeren



Uitwerking casus programmeren.







## Vraag 1.



Er moet een nieuw kamer toegevoegd worden. relatief simpel. Maak een klas met kamer die een enkel paar waardes bevat. Vraag de gebruiker om alle waardes van een kamer, valideer deze waardes en maak een nieuwe kamer aan. 
Voeg ten slot deze kamer toe aan een lijst die globaal wordt opgeslagen zodat deze kamer daadwerkelijk opgeslagen wordt.



## Vraag 2. 



Zuurstof moet berekent worden per kamer. 



Maak een user interface die eerst alle kamers ophaalt en displayd.



Voor de daadwerkelijke vraag zelf maak een functie in de kamer klas die het zuurstof berekent. Omdat we veel data terug willen sturen maak je een nieuwe klas aan genaamt OxygenResult waar informatie van overblijvende zuurstof, max uren en gebruikt zuurstof opgestuurd in kan worden





## Vraag 3

Eerst laad alle data in vanuit een JSON bestand dat zich bevind in de subfolder data. Hierin staan de nodes en edges die later gebruikt worden voor het dijkstra.net algoritme

Dit inladen wordt gedaan via de externe klas JsonLoader.

In een Graph wordt de graph opgeslagen, deze graph bevat meerdere tuple's van strings.
In een Dictionary worden de nodes opgeslagen.
Hierna worden de nodes in de graph gekoppeld.


Nu de graph geinitialiseerd is kan er om data gevraagd worden van de user.
Gebouw, start punt, eind punt. Dit moet allemaal gevalideerd worden

Ten slot wordt het nodige informatie uit de dta graph gehaald, wordt de kortste route via dijksta.net berekent en wordt alles in de console geprint.


## Vraag 4 / 5 / 6
Alle berekeningen staan in equations.cs.

Als eerst kijk of er wel genoeg ruimtes zijn.

Vraag daarna om informatie aan de user, welke kamer, op dag of week basis en begin uur.

Stuur dit op naar equations.cs die alles berekent en een integer terug stuurt.


## Vraag 7
De vraag was onduidelijk. Interpretatie is dat er een reserveringssysteem moest komen en die is gemaakt. Reserveringslogica staat in reservationshelper.cs, grotendeel van de UI code staat in Question_7.cs

Eerst wordt data opgevraagd

Daarna wordt gekeken of de gegeven ruimte op de gegeven tijd vrij is

Indien wel, wordt een reservering aangemaakt
Indien niet. Wordt de gebruiker teruggestuurd.

## Vraag 8
Eerst wordt gevraagt welk gebouw berekent moet worden. Daarna de datum en uur. Hierna wordt elke reservering van die datum / uur opgehaald en wordt de capaciteit bij elkaar opgeteld van elke ruimte die gereserveerd is.
Dit is hoeveel ongeveer aanwezig zijn.

## Vraag 9

Alle reserveringen worden opgehaald van een gegeven maand en elke ruimte per dag wordt in een lijst gezet. Er zijn geen dubbele ruimtes op een dag. Het aantal ruimtes per dag wordt opgeteld en is het totaal aantal bezette dagen voor alle lokalen
Daarna wordt het aantal ruimtes berekent en het aantal dagen in de geselecteerde maand. 
Ten slot wordt de formule berekent.

## Vraag 10
Eerst wordt om data gevraagd, daarna wordt via een paar functies in reservationHelper bepaald of de gevraagde ruimte op de gevraagde tijd beschikbaar is. Indien wel beschikbaar wordt die ruimte gereserveerd

Indien niet neemt de computer over en haalt alle ruimtes op met een capaciteit hoger dan gegeven. Deze lijst wordt gesorteert op basis van capaciteit zodat de laagste capaciteit op index 0 staan. Hierna wordt via een foreach door de lijst geitereerdt en wordt via functies in reservationHelper bepaald of de ruimte rond de gegeven tijd beschikbaar is, indien beschikbaar krijgt de gebruiker een melding dat het reserveren gelukt is.


