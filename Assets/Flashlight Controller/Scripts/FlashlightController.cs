using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class FlashlightController : MonoBehaviour
{
    #region Inspector Fields

    [Header("SFX")]
    [SerializeField] private AudioClip onSFX;  //Suono da riprodurre quando la torcia si accende
    [SerializeField] private AudioClip offSFX; //Suono da riprodurre quando la torcia si spegne

    [Header("Input")]
    [SerializeField] private KeyCode toggleKey = KeyCode.F; //Tasto configurabile per il toggle della torcia

    [Header("References")]
    [Tooltip("Il modello 3D della torcia che deve comparire o scomparire")]
    [SerializeField] private GameObject torchModel; //Riferimento al GameObject che contiene la mesh della torcia
    [Tooltip("Light component da attivare quando la torcia è accesa")]
    [SerializeField] private Light lightSource; //Riferimento al componente Light da abilitare/disabilitare

    [Header("Settings")]
    [SerializeField] private float rotationSpeed = 5f; //Velocità di interpolazione per la rotazione verso la camera

    #endregion

    #region Public Properties

    /// <summary>
    /// Indica se la torcia è attualmente accesa o spenta
    /// </summary>
    public bool IsOn {  get; private set; }

    /// <summary>
    /// Flag per abilitare o disabilitare completamente la torcia
    /// </summary>
    public bool IsEnabled = true;

    #endregion

    //Riferimento al transform della camera principale
    private Transform cameraTransform;

    //Offset iniziale tra la torcia e la camera
    private Vector3 offset;

    //Componente AudioSource usato per riprodurre effetti sonori
    private AudioSource audioSource;

    private void Awake()
    {
        //Ottiene il componente AudioSource, serve per l'audio
        audioSource = GetComponent<AudioSource>();

        // Verifica la presenza della Main Camera in scena
        if (Camera.main == null)
            Debug.LogWarning("Main camera non trovata in scena");

        cameraTransform = Camera.main?.transform;

        //Controlli di sicurezza sugli oggetti assegnati nell' Inspector
        if (torchModel == null)
            Debug.LogError("torchModel non assegnato in inspector");

        if (lightSource == null)
            Debug.LogError("lightSource non assegnata in inspector");
    }

    private void Start()
    {
        //Calcola l'offset solo se la camera esiste
        if (cameraTransform != null)
            offset = transform.position - cameraTransform.position;

        // Assicura che torcia e luce siano spente all'avvio
        SetTorchState(false);
    }

  

    private void Update()
    {
        //Gestisce l'input per il toggle della torcia
        HandleInput();
    }

    private void LateUpdate()
    {
        //se non ha la camera esce subito
        if (cameraTransform == null)
            return;

        //Segui la camera mantenendo offset e interpolando la rotazione
        transform.position = cameraTransform.position + offset;
        transform.rotation = Quaternion.Slerp(transform.rotation, cameraTransform.rotation, rotationSpeed * Time.deltaTime);
    }

    /// <summary>
    /// Controlla il tasto toggle e invoca il toggle se necessario
    /// </summary>
    private void HandleInput()
    {
        //Se la torcia è disattivata o manca la camera non fa nulla
        if (!IsEnabled || cameraTransform == null) 
            return;

        //Come il tasto viene rilasciato cambia stato della torcia
        if (Input.GetKeyDown(toggleKey) && Time.timeScale == 1f)
            ToggleTorch();
    }

    /// <summary>
    /// Alterna lo stato della torcia (accesa/spenta), gestice il modello, la luce e l'audio
    /// </summary>
    private void ToggleTorch()
    {
        IsOn = !IsOn; //Inverte il flag interno
        SetTorchState(IsOn); //aggiorna la visibilità del modello de della luce

        //Sceglie la clip giusta da riprodurre
        var clip = IsOn ? onSFX : offSFX;
        playSFX(clip);
    }

    /// <summary>
    /// Attiva o disattiva il modello 3D e il componente Light
    /// </summary>
    /// <param name="state">true per accendere, false per spegnere</param>
    private void SetTorchState(bool state)
    {
        //Stato modello 3D
        if (torchModel != null)
            torchModel.SetActive(state); //Mostra/nasconde la mesh

        //Stato fonte luminosa
        if (lightSource != null)
            lightSource.enabled = state;//abilita/disabilita la light
    }

    /// <summary>
    /// Riproduce un clip audio una sola volta se ha dei componenti validi
    /// </summary>
    /// <param name="clip">Audioclip da riprodurre</param>
    private void playSFX(AudioClip clip)
    {
        if (audioSource == null || clip == null)
            return;

        audioSource.PlayOneShot(clip);
    }






    /* 
     * ----VECCHIO CODICE----
     * 
    [SerializeField] AudioClip _onSFX; //suono di accensione
    [SerializeField] AudioClip _offSFX; //suono di spegnimento
    [SerializeField] KeyCode _toggleKey; // tasto per il toggle

    private GameObject _cameraObject; //riferimento alla camera principale
    private GameObject _lightSource; //il primo child dell'oggetto, quello che contiene la fonte luminosa
    private AudioSource _audioSource; //componente audio per il suono
    private Vector3 _offset; //la distanza fissa tra la torcia e la camera

    public bool IsOn { get; private set; } //proprietà pubblica in sola lettura per lo stato on/off della torcia
    private readonly float _speed = 5f; //Velocità di rotazione verso la camera

    public bool IsEnabled = true; //flag per abilitare/disabilitare la torcia


    private void Awake()
    {
        _cameraObject = Camera.main.gameObject; //recupera la camera come game object
        _lightSource = transform.GetChild(0).gameObject; //accende il child del transform per individuare la fonte luminosa
        _audioSource = GetComponent<AudioSource>(); //ottiene il componente audiosource collegato allo stesso gameobject
        
    }

    private void Start()
    {
        _lightSource.gameObject.SetActive(false); //disattiva la luce per avere uno stato iniziale disattivo
        _offset = transform.position - _cameraObject.transform.position; //calcola l'ofset iniziale tra la camera e la torcia
    }

    private void Update()
    {
        //per mantenere la torcia allineata alla camera aggiunge l'offset alla posizione della camera
        //effettua una rotazione tramite slerp verso la rotazione della camera moltiplicando la velocità * il time.delta time
        transform.position = _cameraObject.transform.position + _offset;
        transform.rotation = Quaternion.Slerp(transform.rotation, _cameraObject.transform.rotation, _speed * Time.deltaTime);

        if (!IsEnabled) //se falso forza la fonte luminosa spenta e ritorna
        {
            _lightSource.gameObject.SetActive(false);
            IsOn = false;
            return;
        }

        if (Input.GetKeyDown(_toggleKey)) //alla pressione del tasto riproduce una volta l'audiosource
        {
            _audioSource.PlayOneShot(_onSFX);
        }

        if (Input.GetKeyUp(_toggleKey)) //riproduce l'audio source e alterna lo stato stato della torcia con torcia IsOn e SetActive
        {
            _audioSource.PlayOneShot(_offSFX);

            if (IsOn == false)
            {
                _lightSource.gameObject.SetActive(true);
                IsOn = true;
            }
            else
            {
                _lightSource.gameObject.SetActive(false);
                IsOn = false;
            }
        }
    }

    public void PlayFlashlightOffSFX() // ritardo di 2s tra la riproduzione dei suoni di on e off
    {
        _audioSource.PlayOneShot(_onSFX);
        _audioSource.PlayDelayed(2f);
        _audioSource.PlayOneShot(_offSFX);
    }
    */
}
