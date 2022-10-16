# Jogo de xadrez

### Descrição
O jogo de xadrez é um dos jogos mais populares do mundo. Cada jogador controla dezesseis peças que podem ser brancas e pretas, sendo que as brancas devem sempre fazer o primeiro lance.

No transcorrer da partida, quando o rei de um enxadrista é diretamente atacado por uma peça inimiga, é dito que o rei está em xeque. Nesta posição, o enxadrista tem que mover o rei para fora de perigo, capturar a peça adversária que está efetuando o xeque ou bloquear o ataque com uma de suas próprias peças, sendo que esta última opção não é possível se a peça atacante for um cavalo, pois tal peça pode saltar sobre as peças adversárias. 

O objetivo do jogo é dar xeque-mate ao adversário, o que ocorre quando o rei oponente se encontra em xeque e nenhum lance de fuga, defesa ou ataque pode ser realizado para anular o xeque.

### Movimentação das peças

#### Peão
Os Peões movem-se uma casa para frente, e capturam movendo-se uma casa na diagonal. Na jogada inicial de cada peão ele pode mover-se uma ou duas casas.

#### Torre
As Torres movimentam-se nas verticais e horizontais.

#### Cavalo
Os Cavalos movimentam-se uma casa numa direção e outra casa na diagonal, em forma de L. Ou duas casas para um lado e uma pro outro, como podem preferir. É a única peça que pode pular outra peça. Seu xeque não pode ser coberto. As únicas maneiras de se sair do xeque do cavalo é capturando-o ou movendo o Rei.

#### Bispo
Os Bispos movimentam-se pelas diagonais. Os bispos da casa branca, pelas diagonais da casa branca, e os bispos da casa preta, pelas diagonais da casa preta.

#### Rei
O Rei pode movimentar-se apenas uma casa em qualquer direção. Com exceção do roque, que é uma das jogadas especiais que serão abordadas adiante. Não é permitido que os dois reis fiquem colados um no outro. Tem que haver pelo menos uma casa de distância entre os dois reis.

O Rei é a única peça que não pode ser capturada. Quando ele está sendo atacado, ele fica em xeque, e é forçado a sair da situação do xeque.

#### Rainha
A Rainha pode fazer o mesmo movimento do Rei, com apenas uma diferença. Pode se mover em todas as direções, quantas casas quiser.

Podemos dizer, também, que ela faz o movimento da Torre e do Bispo junto que movimenta-se nas horizontais, verticais (movimentos das torres) e nas diagonais (movimento dos bispos).

### Jogadas especiais

#### En passant
Quando um peão está na quinta casa e o peão adversário avança duas casas, o peão da quinta casa pode capturá-lo, passando para a casa vazia (casa de passagem) do segundo peão, movendo-se uma casa na diagonal. Esse movimento só pode ser executado no lance seguinte ao movimento.

#### Roque grande
O Rei move-se duas casas em direção à Torre do lado da Dama, e a Torre junta-se ao Rei.

#### Roque pequeno
O Rei move-se duas casas em direção à Torre do seu lado, e a Torre junta-se ao Rei.

#### Promoção
Quando um peão alcança a oitava casa, ele é promovido a uma Rainha.

### Fim do jogo

#### Xeque mate
Quando o Rei está impossibilitado de sair do Xeque, ele está em Xeque-Mate e a partida é encerrada, com a vitória do jogador adversário.

#### Empate
Existem duas condições que levam a partidade ao empate. A primeira ocorre quando é a vez de um jogador mas ele não pode movimentar nenhuma de suas peças, principalmente o Rei, porque esse, para qualquer casa que for se mover, fica em posição de Xeque. A segunda condição é quando sobram apenas os dois Reis no tabuleiro.

### Detalhes técnicos
O jogo foi desenvolvido em C# com auxílio do Windows Form para criar as interfaces visuais. No seu código fonte, foram tratados vários conceitos técnicos importantes como: interfaces, herança de classes, polimorfismo, sopreposição de métodos, tratativa de excessões, interfaces, classes  e funções abstratas, delegates, structs, entre outros.

Para poder documentar o código fonte deixando-o mais compreensível e explicativo, foi usado o Doxygen. O Doxygen é uma ferramenta capaz de gerar documentações softwares personalizadas, tornando o código auto explicativo.

