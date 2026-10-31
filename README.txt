DNEVNI CITATI - početna .NET MAUI aplikacija (C#)

ŠTA SADRŽI
- Početni ekran za mobilnu aplikaciju
- Primeri citata da aplikacija radi bez backend-a
- Pretragu po tekstu i autoru
- Filter po kategorijama: Vera, Sport, Život, Ljubav
- Lokalno označavanje omiljenih citata dok je aplikacija pokrenuta

KAKO OTVORITI
1. Instaliraj Visual Studio 2022 sa workload-om ".NET Multi-platform App UI development".
2. Otvori DnevniCitati.csproj u Visual Studio-u.
3. Izaberi Android emulator ili povezani Android uređaj.
4. Pokreni projekat.

POVEZIVANJE SA API-JEM
U Services/QuoteService.cs:
- Postavi UseApi = true
- Promeni ApiBaseUrl na adresu backend-a.
- Trenutni endpoint je primer: GET api/quotes.
- Backend treba da vrati JSON niz objekata sa poljima Id, Text, Author, Category.
Primer JSON-a:
[
  {"id":1,"text":"Primer citata","author":"Autor","category":"Život"}
]

Važno:
- 10.0.2.2 se koristi iz Android emulatora da bi se pristupilo računaru na kome radi API.
- Port 5000 je samo primer; koristi port koji ti da kolega.
- Android telefon na istoj Wi-Fi mreži obično treba IP adresu računara, ne 10.0.2.2.
- Za HTTP lokalno testiranje Android konfiguracija može zahtevati dozvolu za cleartext saobraćaj. Za produkciju koristi HTTPS.
- Omiljeni citati se trenutno čuvaju samo u memoriji i resetuju se kad se aplikacija ponovo pokrene. Trajno čuvanje i korisnički nalozi treba da idu preko backend API-ja.
