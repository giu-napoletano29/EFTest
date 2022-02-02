# Roadmap

 1. Studio
 2. Creazione API
 3. Creazione struttura pagine
 4. Creazione layout principale
 5. Creazione logica applicazione
 
 ## Studio
 **API da creare**
 
|Nome|Tipo|Route|Descrizione |Valori di ritorno|
|--|--|--|--|--|
| GetAllBrands()|GET |/brands  |Restituisce tutti i brand	 | Id - Nome brand|
| InsertBrand()|POST |  |Inserisce un brand	 | |
| UpdateBrand()|UPDATE|  |Modifica un brand	 | |
| RemoveBrand()|DELETE|  |Rimuove un brand	 | |
| GetBrandByName()|GET|  |Restituisce i brand con quel nome	 | Lista di brand |
| GetBrandById()|GET|  |Restituisce il brand con quel Id| Brand |
| GetAllProducts()|GET |/products  |Restituisce tutti i prodotti| Lista di prodotti|
| InsertProduct()|POST |  |Inserisce un prodotto| |
| UpdateProduct()|UPDATE|  |Modifica un prodotto| |
| RemoveProduct()|DELETE|  |Rimuove un prodotto| |
| GetAllLeeds()|GET |/leeds  |Restituisce tutte le richieste info| Lista di richieste info|

 **Pagine**
 
|Nome|Route|
|--|--|
| Brands |/brands|
| Insert New brand |/brands/new|
| Modifica brand |/brands/:id/edit|
| Dettaglio brand |/brands/:id|
| Products|/products|
| Insert New product|/products/new|
| Modifica prodotto |/products/:id/edit|
| Dettaglio prodotto (Productname by Brandname)|/products/:id|
| Leeds|/leads|
| Dettaglio leed|/leads/:id|

 **Funzionalità**
 
|Nome|Tipo|Descrizione | API |
|--|--|--|--|
|**Brands**||lista brand|GetAllBrands()|
| Crea nuovo brand |Pulsante  |Porta alla pagina Insert New Brand. l’inserimento dei brand e l’inserimento dei prodotti deve avvenire con un’unica transazione all’API| InsertBrand()|
| Modifica brand |Pulsante  |Porta alla pagina Modifica brand. | UpdateBrand()|
| Rimuovi brand |Pulsante  |Chiede conferma poi elimina il brand. | RemoveBrand()|
| Ricerca brand |Input box|Input box per ricercare il brand per nome |GetBrandByName() |
| Paginazione brand ||Paginazione via server ||
| Dettagli brand | Pulsante |Cliccando sulla riga del Brand porta alla pagina di dettaglio. ||
| Paginazione prodotti||Paginazione via client|GetBrandById()|
|**Products**||lista prodotti|GetAllProdcuts()|
| Filtro per brand ||La pagina conterrà un filtro brand come select |--|
| Ordinamento ||la tabella sarà ordinabile per: Brand, Nome Prodotto, Prezzo. Di default sarà ordinata per Brand + Nome Prodotto ASC. Il campo di ordinamento è selezionabile cliccando il titolo della relativa colonna. Il click sulla colonna già ordinata ne inverte l’ordinamento (asc/desc). |--|
| Crea nuovo prodotto|Pulsante  |Porta alla pagina Insert New Product. La pagina permetterà di creare un nuovo prodotto e associarlo obbligatoriamente ad un’azienda. La selezione dell’azienda avviene mediante una select| InsertProduct()|
| Modifica prodotto |Pulsante  |Porta alla pagina di modifica del Prodotto | UpdateProduct()|
| Rimuovi prodotto|Pulsante  |Chiede conferma poi elimina il prodotto. | RemoveProduct()|
| Dettagli prodotto| Pulsante |Cliccando sulla riga del prodotto porta alla pagina di dettaglio. ||
| Paginazione Info request||Paginazione via client.||
| “Vedi tutte le richieste per questo prodotto”|Pulsante|Porta alla pagina generale di tutti i Leads filtrata per Brand Name e Prodotto||
|**Leeds**||lista leeds|GetAllLeeds()|
| Filtro||La lista sarà filtrabile per Nome Brand (select), Nome Prodotto (input text)||
| Ordinamento||La tabella sarà ordinabile per Data dalla  più alla meno recente. Di default: dalla più recente||
| Dettaglio Leed| Pulsante |Cliccando sulla riga del leed porta alla pagina di dettaglio. ||
