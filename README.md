# RistoranteWebApp

RistoranteWebApp è un'applicazione Web scritta in C# con .NET Core per la gestione di un ristorante, sviluppata come progetto 
finale per il corso di Paradigmi Avanzati di Programmazione modulo Programmazione Enterprise, dell'Università degli Studi di Camerino, 
tenuto dal Prof. Federico Paoloni.

In questa App è possibile registrare e autenticare clienti, richiedere degli ordini e visualizzare lo storico degli ordini effettuati.
Per gli utenti amministratori è possibile creare portate e visualizzare gli ordini effettuati dai clienti.

A seguire le istruzioni dettagliate su come provare il progetto.

## Installazione di SQL Server e SQL Management Studio

### Passaggi per installare SQL Server:

1. Scaricare il file di installazione di SQL Server dalla [pagina ufficiale di download](https://www.microsoft.com/it-it/sql-server/sql-server-downloads).
2. Eseguire il file di installazione e seguire le istruzioni della procedura guidata.
3. Durante l'installazione, selezionare l'opzione per installare un'istanza SQL Server.

### Passaggi per installare SQL Management Studio:

1. Scaricare SQL Server Management Studio (SSMS) dalla [pagina ufficiale di download](https://docs.microsoft.com/it-it/sql/ssms/download-sql-server-management-studio-ssms).
2. Eseguire il file di installazione e seguire le istruzioni della procedura guidata.

## Configurazione del Server SQL

### Avviare un server SQL in locale:

1. Aprire SQL Server Management Studio.
2. Connettersi all'istanza del server locale (di solito `localhost`).

### Creazione del database

1. Trovare il file di dump del database nella cartella `SqlScripts -> SchemaDB.sql`.
2. Aprire il file `SchemaDB.sql` con SQL Management Studio.
3. Eseguire lo script per creare il database `Ristorante` e le relative tabelle.

### Creazione di un nuovo Account di Accesso

1. Aprire SQL Server Management Studio e connettersi al server locale.
2. Espandere la cartella `Sicurezza` e fare clic con il pulsante destro del mouse su `Account di accesso`.
3. Selezionare `Nuovo Account di Accesso` e poi 'Autenticazione di SQL Server'.
4. Inserire le seguenti informazioni:
    - Nome account: `RistoranteWebApp`
    - Password: `paradigmi123`
5. Selezionare il database `Ristorante` come database predefinito.
6. Assegnare il ruolo `db_owner` al nuovo account per il database `Ristorante`.

### (Opzionale) Caricamento dei dati di esempio

1. Trovare il file `EsempioIstanzaDB.sql` nella cartella `SqlScripts`.
2. Aprire il file `EsempioIstanzaDB.sql` con SQL Management Studio.
3. Eseguire lo script per popolare il database con dati di esempio.

### Dati di esempio

#### Tabella `Utenti`

| Id  | Email                     | Nome  | Cognome | Password                                                                                                    | Ruolo |
|-----|---------------------------|-------|---------|-------------------------------------------------------------------------------------------------------------|-------|
| 1   | mario.rossi@example.com   | Mario | Rossi   | AQAAAAIAAYagAAAAEJJIddXv0MMUkLsH+TNqvxnPIjTpWeHI9r3KHsX3FdTglUMebI2gF2mY1cLgVbUAiQ==                        | 2     |
| 2   | luigi.bianchi@example.com | Luigi | Bianchi | AQAAAAIAAYagAAAAEEVqdXgt03rqcFWqEAmrm1FgA3hIdDgss8lRwp0L4UlVJto8eUVZXoCgbnkUJ1GfDg==                        | 1     |
| 3   | anna.verdi@example.com    | Anna  | Verdi   | AQAAAAIAAYagAAAAEL9UqU3yM9MXIKHiilPoVS5GWK4/ffkdBZOWtcm2hiqLYl9W7vczIFdrhR1X4lxtSQ==                        | 1     |

Essendo le password salvate in formato hash, è possibile accedere con le seguenti credenziali:
- (Amministratore)   Email: mario.rossi@example.com     - Password: Paradigmi123
- (Cliente)          Email: luigi.bianchi@exaple.com    - Password: Paradigmi456
- (Cliente)          Email: anna.verdi@example.com      - Password: Paradigmi789

#### Tabella `Portate`

| Id  | Nome                   | Prezzo | Tipo |
|-----|------------------------|--------|------|
| 1   | Carbonara              | 12.00  | 1    |
| 2   | Lasagna                | 14.00  | 1    |
| 3   | Risotto ai funghi      | 13.00  | 1    |
| 4   | Spaghetti alle vongole | 15.00  | 1    |
| 5   | Zuppa di pesce         | 20.00  | 1    |
| 6   | Filetto di manzo       | 25.00  | 2    |
| 7   | Pollo alla cacciatora  | 18.00  | 2    |
| 8   | Salmone                | 20.00  | 2    |
| 9   | Insalata Greca         | 6.00   | 3    |
| 10  | Insalata Caprese       | 6.00   | 3    |
| 11  | Prosciutto e melone    | 7.00   | 3    |
| 12  | Tiramisù               | 6.00   | 4    |
| 13  | Gelato                 | 4.50   | 4    |
| 14  | Cannoli                | 5.00   | 4    |
| 15  | Panna cotta            | 5.50   | 4    |

#### Tabella `Indirizzi`

| Id  | Via       | NumeroCivico | Città  | CAP   |
|-----|-----------|--------------|--------|-------|
| 1   | Via Roma  | 1            | Roma   | 00100 |
| 2   | Via Milano| 10           | Milano | 20100 |
| 3   | Via Napoli| 20           | Napoli | 80100 |


#### Tabella `Ordini`

| Numero | Data                | IdIndirizzo | IdUtente |
|--------|---------------------|-------------|----------|
| 1      | 2024-01-01 12:00:00 | 1           | 3        |
| 2      | 2024-01-02 13:00:00 | 2           | 2        |
| 3      | 2024-01-03 14:00:00 | 1           | 3        |
| 4      | 2024-01-04 18:30:00 | 1           | 2        |
| 5      | 2024-01-05 19:00:00 | 1           | 3        |
| 6      | 2024-01-06 20:00:00 | 3           | 3        |

#### Tabella `VociOrdine`

| NumeroOrdine | IdPortata | Quantità |
|--------------|-----------|----------|
| 1            | 1         | 2        |
| 1            | 2         | 1        |
| 2            | 3         | 3        |
| 2            | 4         | 2        |
| 3            | 5         | 3        |
| 3            | 6         | 2        |
| 3            | 9         | 4        |
| 3            | 15        | 2        |
| 4            | 7         | 1        |
| 4            | 8         | 2        |
| 5            | 9         | 1        |
| 5            | 10        | 2        |
| 6            | 11        | 2        |
| 6            | 12        | 1        |
| 6            | 13        | 2        |

## Avvio dell'applicazione

1. Aprire il progetto `RistoranteWebApp` con Visual Studio.
2. Configurare la stringa di connessione al database nel file `appsettings.json`, se necessario.
3. Eseguire l'applicazione dal file `Program.cs` presente nel progetto Unicam.Ristorante.Web, usando come profilo di avvio `http`.
4. Aprire il browser e collegarsi all'interfaccia Swagger all'indirizzo `http://localhost:5027/swagger/index.html`.
