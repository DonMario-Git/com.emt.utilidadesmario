using EMT.UtilidadesMario;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace EMT
{
    public class SonidoManager : MonoBehaviour
    {
        public static SonidoManager singleton;

        public GameObject AudioSoursePrefab;

        public Queue<AudioSource> QueueAS_Desocupados;
        public Queue<AudioSource> QueueAS_Ocupados;

        public int AudiosMaximos = 30;

        public AudioSource AudioSourceMusica;
        public AudioSource AudioSourceAmbiente;

        public AudioClip[] sonidos;


        private void Awake()
        {
            QueueAS_Desocupados = new Queue<AudioSource>();
            QueueAS_Ocupados = new Queue<AudioSource>();

            if (singleton != null)
            {
                Destroy(singleton.gameObject);
                singleton = this;
            }

            singleton = this;
        
            DontDestroyOnLoad(gameObject);

            for (int i = 0; i < AudiosMaximos; i++)
            {
                AudioSource newAudioSourse = Instantiate(AudioSoursePrefab, transform.position, Quaternion.identity).GetComponent<AudioSource>();
                newAudioSourse.transform.SetParent(transform);
                QueueAS_Desocupados.Enqueue(newAudioSourse);
            }          
        }

        public void CambiarAmbiente(AudioClip clip, float volumen, float pitch)
        {
            if (clip != AudioSourceAmbiente.clip)
            {
                AudioSourceAmbiente.Stop();
                AudioSourceAmbiente.clip = clip;
                AudioSourceAmbiente.volume = volumen;
                AudioSourceAmbiente.pitch = pitch;
                AudioSourceAmbiente.Play();
            }
        }

        /// <summary>
        /// Cambia la musica con una transicion de volumen
        /// </summary>
        /// <param name="clip"></param>
        /// <param name="duration"></param>
        /// <param name="finalVolume"></param>
        public void CambiarMusica(AudioClip clip, float duration, float finalVolume)
        {
            if (clip != AudioSourceMusica.clip)
            {
                if (corrutinaMusica != null) StopCoroutine(corrutinaMusica);
                corrutinaMusica = StartCoroutine(TransicionarVolumen(AudioSourceMusica, 0f, duration, () => {
                    AudioSourceMusica.clip = clip;
                    AudioSourceMusica.Play();
                    StartCoroutine(TransicionarVolumen(AudioSourceMusica, finalVolume, duration));
                }));
            }
        }

        private Coroutine corrutinaMusica;
    
        IEnumerator TransicionarVolumen(AudioSource audioSource, float volumenFinal, float duracion, Action OnComplete = null)
        {
            if (duracion < 0)
            {
                Debug.LogError("La duracion no puede ser menor a 0");
                yield break;
            }

            if (volumenFinal.EstaEntreRango(0, 1.1f))
            {
                Debug.LogError($"El volumen de {audioSource.name} esta fuera de los limites");
                yield break;
            }

            if (duracion == 0)
            {
                audioSource.volume = volumenFinal;
                OnComplete?.Invoke();
                yield break;
            }

            float time = 0;
            float volumenInicial = audioSource.volume;

            while (time < 1)
            {
                time += Time.unscaledDeltaTime / duracion;
                time = time > 1 ? 1 : time;

                audioSource.volume = Mathf.Lerp(volumenInicial, volumenFinal, time);

                yield return null;
            }

            OnComplete?.Invoke();
        }

        public void CambiarVolumenMusica(float volumenFinal, float duracion)
        {
            if (corrutinaMusica != null) StopCoroutine(corrutinaMusica);
            corrutinaMusica = StartCoroutine(TransicionarVolumen(AudioSourceMusica, volumenFinal, duracion));
        }

        public void ReproducirSonido(AudioClip clip, float volumen, float pitch, Transform TransformFor3D, bool Loop = false)
        {
            if (QueueAS_Desocupados.Count == 0)
            {
                while (QueueAS_Ocupados.Count > 0)
                {
                    QueueAS_Desocupados.Enqueue(QueueAS_Ocupados.Dequeue());     
                }
            }

            AudioSource newSound;

            if (Loop)
            {
                newSound = Instantiate(AudioSoursePrefab, transform.position, Quaternion.identity).GetComponent<AudioSource>();
                newSound.transform.SetParent(TransformFor3D);
                newSound.loop = true;
            }
            else
            {
                newSound = QueueAS_Desocupados.Dequeue();            
                QueueAS_Ocupados.Enqueue(newSound);
            }

            newSound.clip = clip;
            newSound.volume = volumen;
            newSound.pitch = pitch;

            if (TransformFor3D != null)
            {
                newSound.spatialBlend = 1;
                newSound.transform.position = TransformFor3D.position;
            }
            else
            {
                newSound.spatialBlend = 0;
                newSound.transform.position = Vector2.zero;
            }

            newSound.Play();
        }
    }
}



