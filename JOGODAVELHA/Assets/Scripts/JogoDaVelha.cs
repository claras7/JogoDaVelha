using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class JogoDaVelha : MonoBehaviour
{
    public Button[] casas; // Botões com TextMeshPro como filho
    private string jogadorAtual = "X";
    private bool jogoAtivo = true;

    public TMP_Text textoStatus;         // Texto fora do tabuleiro que mostra "Vez do Jogador X"
    public Button botaoReiniciar;        // Botão de reiniciar

    public void ClicarNaCasa(int index)
    {
        if (!jogoAtivo) return;

        // Pegando o TextMeshProUGUI corretamente
        TMP_Text textoBotao = casas[index].GetComponentInChildren<TMP_Text>();

        if (textoBotao.text == "")
        {
            textoBotao.text = jogadorAtual;
            casas[index].interactable = false;

            if (VerificarVencedor())
            {
                textoStatus.text = $"Jogador {jogadorAtual} venceu!";
                jogoAtivo = false;
                botaoReiniciar.gameObject.SetActive(true);
            }
            else if (VerificarEmpate())
            {
                textoStatus.text = "Empate!";
                jogoAtivo = false;
                botaoReiniciar.gameObject.SetActive(true);
            }
            else
            {
                jogadorAtual = (jogadorAtual == "X") ? "O" : "X";
                textoStatus.text = $"Vez do Jogador {jogadorAtual}";
            }
        }
    }

    bool VerificarVencedor()
    {
        int[,] combinacoes = new int[,]
        {
            {0,1,2}, {3,4,5}, {6,7,8}, // Linhas
            {0,3,6}, {1,4,7}, {2,5,8}, // Colunas
            {0,4,8}, {2,4,6}           // Diagonais
        };

        for (int i = 0; i < combinacoes.GetLength(0); i++)
        {
            string a = casas[combinacoes[i, 0]].GetComponentInChildren<TMP_Text>().text;
            string b = casas[combinacoes[i, 1]].GetComponentInChildren<TMP_Text>().text;
            string c = casas[combinacoes[i, 2]].GetComponentInChildren<TMP_Text>().text;

            if (a == jogadorAtual && b == jogadorAtual && c == jogadorAtual)
                return true;
        }
        return false;
    }

    bool VerificarEmpate()
    {
        foreach (Button casa in casas)
        {
            if (casa.GetComponentInChildren<TMP_Text>().text == "")
                return false;
        }
        return true;
    }

    public void ReiniciarJogo()
    {
        foreach (Button casa in casas)
        {
            casa.GetComponentInChildren<TMP_Text>().text = "";
            casa.interactable = true;
        }

        jogadorAtual = "X";
        jogoAtivo = true;
        textoStatus.text = "Vez do Jogador X";
        botaoReiniciar.gameObject.SetActive(false);
    }
}


