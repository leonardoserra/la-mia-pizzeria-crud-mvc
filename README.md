# la-mia-pizzeria-crud-mvc  PARTE 1
IMPORTANTE: Ricordatevi di sganciare la vostra vecchia repository GIT e di crearne una nuova per questo esercizio, che prosegue il lavoro della pizzeria, dove lo avevate lasciato. Potete eliminare il progetto Razor da questa esercitazione.  

Ciao ragazzi, andiamo avanti con l’applicazione per gestire la nostra pizzeria. Lo scopo di oggi è quello di rendere dinamici i contenuti che abbiamo come html statico nella pagina con la lista delle pizze.  
- Creiamo prima un nostro controller chiamato PizzaController e utilizziamo lui d’ora in avanti. L’elenco delle pizze ora va passato come model dal controller, e la view deve utilizzarlo per mostrare l’html corretto.  
- Gestiamo anche la possibilità che non ci siano pizze nell’elenco: in quel caso dobbiamo mostrare un messaggio che indichi all’utente che non ci sono pizze presenti nella nostra applicazione.  
- Ogni pizza dell’elenco avrà un pulsante che se cliccato ci porterà a una pagina che mostrerà i dettagli di quella singola pizza. Dobbiamo quindi inviare l’id come parametro dell’URL, recuperarlo con la action, caricare i dati della pizza ricercata e passarli come model. La view a quel punto li mostrerà all’utente con la grafica che preferiamo.  
> Ps. visto che abbiamo cambiato il controller sul quale lavoriamo, ricordiamoci di cambiare anche il “mapping di default” dei controller...altrimenti quale pagina viene caricata se richiamo l’url “/” della nostra webapp?  
Piccolo dettaglio…ricordatevi che i dati delle pizze devono essere in un database…quindi dobbiamo usare Entity Framework!
  
# la-mia-pizzeria-crud-mvc  PARTE 2
Ciao ragazzi, nuove implementazioni per la nostra applicazione.  
Abbiamo la lista delle pizze, abbiamo i dettagli delle pizze...perchè non realizzare la pagina per la creazione di una nuova pizza?  
- Aggiungiamo quindi tutto il codice necessario per mostrare il form per la creazione di una nuova pizza e per il salvataggio dei dati in tabella tramite Entity Framework.  
- Nella index creiamo ovviamente il bottone “Crea nuova pizza” che ci porta a questa nuova pagina creata.  
- Ricordiamoci che l’utente potrebbe sbagliare inserendo dei dati : gestiamo quindi la validazione!  
  
Ad esempio verifichiamo che :  
> i dati della pizza siano tutti presenti  
> il campi di testo non superino una certa lunghezza  
> il prezzo abbia un valore valido (ha senso una pizza con prezzo minore o uguale a zero?)  
  
BONUS  
- Prevediamo una validazione in più: vogliamo che la descrizione della pizza contenga almeno 5 parole.

  
# la-mia-pizzeria-crud-mvc PARTE 3  

Ciao ragazzi, andiamo avanti col nostro progetto.  
Completiamo le pagine di gestione delle nostre pizze!  
Abbiamo la pagina con la lista di tutte le pizze, quella con i dettagli della singola pizza, quella per crearla...cosa manca?  
Dobbiamo realizzare :  
- pagina di modifica di una pizza  
- cancellazione di una pizza cliccando un pulsante presente nella grafica di ogni singolo prodotto mostrato nella lista in homepage  
**BONUS:**  
> - Implementare i concetti di Dependency Injection visto, per esempio facendo i vostri CustomLoggers. (Riflettete sul discorso dell'importanza delle Interfacce e poi sulla DI applicata nel contesto di web con numerosi controllers)  
> - Provate a semplificare la sintassi dei vostri controller aggiungendo come DI il vostro database.

# la-mia-pizzeria-crud-mvc PARTE 4  
  
Ciao ragazzi, nuova giornata di lavoro!  
Oggi sviluppiamo un’importante funzionalità : aggiungiamo una categoria alle nostre pizze (”Pizze classiche”, “Pizze bianche”, “Pizze vegetariane”, “Pizze di mare”, ...).  
Dobbiamo quindi predisporre tutto il codice necessario per poter collegare una categoria a una pizza (in una relazione 1 a molti, cioè una pizza può avere una sola categoria, e una categoria può essere collegata a più pizze).  
  
1. Tramite migration dobbiamo creare la tabella per le categorie. Popoliamola a mano con i valori elencati precedentemente.  
2. Aggiungiamo poi l’informazione della categoria nelle varie pagine :  
> - nei dettagli di una singola pizza (nell’admin) mostrare la sua categoria  
> - quando si crea/modifica una pizza si deve poter selezionare anche la sua categoria
  
# la-mia-pizzeria-crud-mvc PARTE 5  
  
Oggi sviluppiamo un’altra importante funzionalità: aggiungiamo gli ingredienti alle nostre pizze.  
1. Relazione: Una pizza può avere più ingredienti, e un ingrediente può essere presente in più pizze.  
2. Creiamo quindi il Model necessario e la migration.  
3. Aggiungiamo poi il codice al controller (e alle view) per la gestione degli ingredienti quando creiamo, modifichiamo o visualizziamo una pizza.


# la-mia-pizzeria-crud-mvc PARTE 6  
  
Proteggiamo le nostre pagine di gestione delle pizze!  
Aggiungiamo tutto il necessario per la login e la registrazione.  

Ricordiamoci poi di bloccare l’accesso al nostro controller delle Pizze con [Authorize] e creiamo una HomePage dove verrà rimandato l’utente dopo il logout.  
Facciamo in modo che gli utenti con role USER possano accedere solo alla pagina con l’elenco delle pizze e ai dettagli della singola pizza. Mentre gli utenti con role ADMIN devono poter creare, modificare e cancellare le pizze.  
  
BONUS:  
> Provate a fare in modo che un utente quando si registra per la prima volta gli venga associato il ruolo di default come "USER"  

