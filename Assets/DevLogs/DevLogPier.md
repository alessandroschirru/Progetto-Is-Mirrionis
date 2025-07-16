# DevLog Pier 16/07/2025

## Aggiornamento ScenaTestPier

1. Aggiunto alla mia scena una trigger area con relativo script multiuso(modificabile all'occorrenza) per:
   1. Cambiare colore ad una luce per indicare il corretto posizionamento di un blocco.
   2. Posizionare il blocco secondo le coordinate X,Y,Z della trigger Area.
   3. Cambia il layer del blocco da "Grabbable" a "Default" in modo che non possa essere spostato inavvertitamente da giocatore.
2. Inserito un primo TextMeshPro nel Canvas dell'HUD per far comparire dei suggerimenti testuali quando un oggetto interagibile è al centro della visuale.
3. Cambiato forma al puntatore della visuale da quadrato ad un pallino.
---
### Da perfezionare:

- Aggiornare la riga:

```csharp
rb.rotation = Quaternion.Euler(0f, 0f, 0f);
```
in modo che all'ingresso della trigger area il blocco ruoti nella posizione corretta e "inchiodarlo" in posizione in modo che resti tangibile ma diventi inamovibile.
---
### Ulteriori Test da eseguire e aggiunte da fare(WIP):

1. Creare più oggetti con cui costruire una struttura di prova.
2. Far si che l'ingresso nei Puzzle- Trigger faccia comparire un hint testuale alla possibilità di interagire coi puzzle.
3. Inserire una logica per distinguere tra "Fotografie", "Pagine" e "Puzzle" in modo che l'hint testuale cambi di conseguenza.
4. Aggiungere un pacchetto di puntatori al progetto o disegnarne uno.
5. Effetti sonori
6. Iniziare a costruire l'ambientazione.