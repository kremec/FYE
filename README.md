# FYE
Program je namenjen vizualizaciji in razumevanju [sprememb obračunavanja omrežnine](https://www.uro.si/prenova-omre%C5%BEnine/novi-%C4%8Dasovni-bloki#). 

## Dislaimer
To je hobi projekt namenjen izključno osebni uporabi. Tako nihče ne zagotavlja natančnosti ali pravilnosti vsebine - uporaba programa le na lastno odgovornost.

## Usage
1. Pridobitev podatkov iz [mojelektro](https://mojelektro.si/) spletne strani\
Izvozite podatke porabe elektrike (za vsa leta, ki jih želite analizirati) v `.json` datoteke
2. Pridobitev licenčnega ključa\
Pred uporabo programa je potrebno [Syncfusion](https://www.syncfusion.com/) licenčni ključ (možno ga je pridobiti zastonj ob prijavi na "Community edition") shraniti v datoteko `SyncfusionLicence.json`, ki naj se nahaja v istem imeniku kot sam program:
```json
{
  "key": "LICENČNI_KLJUČ"
}
```
3. Uporaba programa\
Najprej uvozite (eno ali več) JSON datotek z podatki iz mojelektro. Zatem lahko združite več uvoženih JSON datotek v eno (za lažjo nadaljnjo uporabo).\
Nato lahko v živo predogledate stroške v izbranem časovnem okviru in za različne nastavitve časovnih blokov (glejte povezavo zgoraj za pojasnitev sprememb in terminologije).\
Nazadnje lahko zaženete testiranje vseh možnih konfiguracij časovnih blokov in njihovih ocenjenih stroškov za dane uvožene podatke, ki se izvozi v tekstovno `.txt` datoteko.

## Notes
Ogleda vrednega [spletna stran](https://plesko.si/elektr/omreznina/) z drugačnimi zmožnostmi analize.
