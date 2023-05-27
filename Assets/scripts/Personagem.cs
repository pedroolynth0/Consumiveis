using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Personagem : MonoBehaviour
{
    Vector3 posicao;
    public int velocidade = 5;
    public int pontos = 0;
    public TextMeshProUGUI textoPontos; // Referência ao componente TextMeshProUGUI
    private bool aumentarVelocidade = false;
    private float duracaoAumentoVelocidade = 4f;
    private float tempoRestanteAumentoVelocidade = 0f;

    public Transform cameraPivot; // Objeto vazio para posicionar e rotacionar a câmera
    public float cameraDistance = 5.0f; // Distância da câmera em relação ao personagem
    public float cameraHeight = 2.0f; // Altura da câmera em relação ao personagem

    void Start()
    {
        posicao = this.transform.position;
        InvokeRepeating("movimenta", 0, 1 / 30);
    }

    void Update()
    {
        posicao = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (aumentarVelocidade)
        {
            tempoRestanteAumentoVelocidade -= Time.deltaTime;

            if (tempoRestanteAumentoVelocidade <= 0f)
            {
                velocidade = 5; // Volta para a velocidade normal após a duração do aumento
                aumentarVelocidade = false;
            }
        }

        this.transform.Translate(posicao * Time.deltaTime * velocidade);

        // Atualiza a posição e rotação do objeto vazio (cameraPivot) em relação ao personagem
        cameraPivot.position = transform.position;
        cameraPivot.rotation = Quaternion.Euler(0, transform.eulerAngles.y, 0);

        // Calcula a posição da câmera em relação ao objeto vazio
        Vector3 cameraPosition = cameraPivot.position - cameraPivot.forward * cameraDistance;
        cameraPosition.y += cameraHeight;

        // Atualiza a posição da câmera
        Camera.main.transform.position = cameraPosition;

        // Faz a câmera olhar para o objeto vazio
        Camera.main.transform.LookAt(cameraPivot);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Moeda"))
        {
            // Incrementa a quantidade de pontos
            pontos++;

            // Remove o objeto da cena
            Destroy(other.gameObject);

            // Atualiza o texto com a nova quantidade de pontos
            textoPontos.text = "Pontos: " + pontos;
        }
        else if (other.CompareTag("Velocidade"))
        {
            // Aumenta a velocidade por 4 segundos
            velocidade *= 2;
            aumentarVelocidade = true;
            tempoRestanteAumentoVelocidade = duracaoAumentoVelocidade;

            // Remove o objeto da cena
            Destroy(other.gameObject);
        }
    }
}
