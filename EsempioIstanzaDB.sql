USE [Ristorante]
GO

-- Deleting existing data from tables
DELETE FROM [dbo].[VociOrdine]
DELETE FROM [dbo].[Ordini]
DELETE FROM [dbo].[Portate]
DELETE FROM [dbo].[Indirizzi]
DELETE FROM [dbo].[Utenti]
GO

-- Portate table
SET IDENTITY_INSERT [dbo].[Portate] ON
GO
INSERT INTO [dbo].[Portate] ([Id], [Nome], [Prezzo], [Tipo]) 
VALUES 
(1, 'Carbonara', 12.00, 1),
(2, 'Lasagna', 14.00, 1),
(3, 'Risotto ai funghi', 13.00, 1),
(4, 'Spaghetti alle vongole', 15.00, 1),
(5, 'Zuppa di pesce', 20.00, 1),
(6, 'Filetto di manzo', 25.00, 2),
(7, 'Pollo alla cacciatora', 18.00, 2),
(8, 'Salmone', 20.00, 2),
(9, 'Insalata Greca', 6.00, 3),
(10, 'Insalata Caprese', 6.00, 3),
(11, 'Prosciutto e melone', 7.00, 3),
(12, 'Tiramisù', 6.00, 4),
(13, 'Gelato', 4.50, 4),
(14, 'Cannoli', 5.00, 4),
(15, 'Panna cotta', 5.50, 4);
GO
SET IDENTITY_INSERT [dbo].[Portate] OFF
GO

-- Indirizzi table
SET IDENTITY_INSERT [dbo].[Indirizzi] ON
GO
INSERT INTO [dbo].[Indirizzi] ([Id], [Via], [NumeroCivico], [Citta], [CAP])
VALUES
(1, 'Via Roma', '1', 'Roma', '00100'),
(2, 'Via Milano', '10', 'Milano', '20100'),
(3, 'Via Napoli', '20', 'Napoli', '80100');
GO
SET IDENTITY_INSERT [dbo].[Indirizzi] OFF
GO

-- Utenti table
SET IDENTITY_INSERT [dbo].[Utenti] ON
GO
INSERT INTO [dbo].[Utenti] ([Id], [Email], [Nome], [Cognome], [Password], [Ruolo])
VALUES
(1, 'mario.rossi@example.com', 'Mario', 'Rossi', 'Password123', 2),
(2, 'luigi.bianchi@example.com', 'Luigi', 'Bianchi', 'Password456', 1),
(3, 'anna.verdi@example.com', 'Anna', 'Verdi', 'Password789', 1);
GO
SET IDENTITY_INSERT [dbo].[Utenti] OFF
GO

-- Ordini table
SET IDENTITY_INSERT [dbo].[Ordini] ON
GO
INSERT INTO [dbo].[Ordini] ([Numero], [Data], [IdIndirizzo], [IdUtente])
VALUES
(1, '2024-01-01 12:00:00', 1, 3),
(2, '2024-01-02 13:00:00', 2, 2),
(3, '2024-01-03 14:00:00', 1, 3),
(4, '2024-01-04 18:30:00', 1, 2),
(5, '2024-01-05 19:00:00', 1, 3),
(6, '2024-01-06 20:00:00', 3, 3);
GO
SET IDENTITY_INSERT [dbo].[Ordini] OFF
GO

-- VociOrdine table
INSERT INTO [dbo].[VociOrdine] ([NumeroOrdine], [IdPortata], [Quantita])
VALUES
(1, 1, 2), -- Order 1 with Carbonara
(1, 2, 1), -- Order 1 with Lasagna
(2, 3, 3), -- Order 2 with Risotto ai funghi
(2, 4, 2), -- Order 2 with Spaghetti alle vongole
(3, 5, 3), -- Order 3 with Zuppa di pesce
(3, 6, 2), -- Order 3 with Filetto di manzo
(3, 9, 4), -- Order 3 with Insalata Greca
(3, 15, 2), -- Order 4 with Panna Cotta
(4, 7, 1), -- Order 4 with Pollo alla cacciatora
(4, 8, 2), -- Order 4 with Salmone
(5, 9, 1), -- Order 5 with Carbonara
(5, 10, 2), -- Order 5 with Insalata Caprese
(6, 11, 2), -- Order 6 with Prosciutto e melone
(6, 12, 1), -- Order 6 with Tiramisù
(6, 13, 2); -- Order 6 with Gelato
GO