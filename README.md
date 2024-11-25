## Esittele dokumentissa sovelluksen rakenne, sen tarjoamat rajapinnat ja kerro kuinka sitä käytetään. Kuinka asiakas saa luotua käyttäjätunnuksen ja kuinka tämän käyttäjätunnuksen avulla sovelluksen muita toimintoja pystyy käyttämään.

### Rakenne:
Rakenne koostuu Controllereista, Serviceistä, Repositoryista, ServiceContextistä ja User sekä Message luokista. Myös Middlewaresta: ApikeyMiddleware, BasicAuthenticationHandler ja UserAuthenticationService.

### Rajapinnat:
Repositoryilla, serviceillä, UserAuthenticationServicellä on omat rajapinnat jotka määrittävät mitä niissä pitää olla.

### Kuinka käytetäätään / Kuinka asiakas luo käyttäjän ja kuinka tämän käyttäjätunnuksen avulla sovelluksen muita toimintoja pystyy käyttämään:

Käyttäjä luo käyttäjän UserControllerin PostUser methodilla joka käy UserServicen NewUser Methodin jossa käy UserRepositoryssa katsomassa GetUserAsync methodilla, joka hakee MessageServicellä tiedon onko tietokannassa jo semmoista käyttäjää.
Jonka jälkeen UserServicessä käydään authenticationServicessä tekemässä UserCredentialsit jossa myös tarkistetaan onko jo samoilla tunnuksella tehty käyttäjää. UserCredentialsissa tehdään uusi käyttäjä, sille kaikki tarvittavat tiedot + salasanan hashaus.
Kun nämä pätevät ja käyttäjänimi on uniikki niin tehdään käyttäjä ja controllerissa palautetaan PostUser mehtodissa CreatedAt Action(nameof(GetUser),new {id = newUser.UserName}, newUser);.

### Sovelluksessa pystyy lisämään, poistamaan, hakemaan, muokkaamaan käyttäjiä.

Käyttäjätunnuksen avulla sovelluksella pystyy lähettämään viestejä, poistamaan viestejä, hakemaan viestejä, hakemaan viestejä usernamen:n ja id:n  perusteella, hakemaan viestejä viestin sisällön perusteella, hakemaan vastaanotetut viestit usernamen perusteella,
muokkaamaan viestejä idn perusteella.



