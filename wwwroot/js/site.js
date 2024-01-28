document.addEventListener("DOMContentLoaded", function () {
    var cells = document.querySelectorAll("#labirinto td");
    cells.forEach(function (cell) {
        if (!cell.classList.contains("inicio") || !cell.classList.contains("fim")) {
            cell.addEventListener("dragleave", function () {
                if (!showing) {
                        if (this.classList.contains("parede")) {
                            this.classList.remove("parede");
                            this.classList.add("caminho");
                        } else if (this.classList.contains("caminho") || this.classList.contains("visitada") || this.classList.contains("path")) {
                            this.classList.remove("caminho");
                            this.classList.remove("visitada");
                            this.classList.remove("path");
                            this.classList.add("parede");
                        }
                }
            });
        }
        if (!cell.classList.contains("inicio") || !cell.classList.contains("fim")) {
            cell.addEventListener("click", function () {
                if (!showing) {
                    if (this.classList.contains("parede")) {
                        this.classList.remove("parede");
                        this.classList.add("caminho");
                    } else if (this.classList.contains("caminho") || this.classList.contains("visitada") || this.classList.contains("path")) {
                        this.classList.remove("caminho");
                        this.classList.remove("visitada");
                        this.classList.remove("path");
                        this.classList.add("parede");
                    }
                }
            });
        }
    });

    var celulasLabirinto = document.querySelectorAll("#labirinto td");
    document.getElementById("limparLabirinto").addEventListener("click", function () {
        if (!showing) {
            i = 0
            celulasLabirinto.forEach(function (celula) {
                celula.classList.remove("inicio");
                celula.classList.remove("parede");
                celula.classList.remove("fim");
                celula.classList.remove("visitada");
                celula.classList.remove("path");
                celula.classList.add("caminho");
                if (i == 899) {
                    celula.className = "inicio"
                    celula.setAttribute("id", "inicio")
                    celula.setAttribute("draggable", "true")
                    celula.setAttribute("ondragstart", "drag(event)")
                    celula.setAttribute("ondrop", "")
                }
                if (i == 950) {
                    celula.className = "fim"
                    celula.setAttribute("id", "fim")
                    celula.setAttribute("draggable", "true")
                    celula.setAttribute("ondragstart", "drag(event)")
                    celula.setAttribute("ondrop", "")
                }
                i++
            });
        }
    });
    var celulasLabirinto = document.querySelectorAll("#labirinto td");
    document.getElementById("limparparede").addEventListener("click", function () {
        if (!showing) {
            celulasLabirinto.forEach(function (celula) {
                if (celula.classList.contains("parede")) {
                    celula.classList.remove("parede");
                    celula.classList.add("caminho");
                }
            });
        }
    });
    var celulasLabirinto = document.querySelectorAll("#labirinto td");
    document.getElementById("limparresultado").addEventListener("click", function () {
        if (!showing) {
            celulasLabirinto.forEach(function (celula) {
                if (celula.classList.contains("path") || celula.classList.contains("visitada")) {
                    celula.classList.remove("visitada");
                    celula.classList.remove("path");
                    celula.classList.add("caminho");
                }
            });
        }
    });
    var botao = document.getElementById("lista")
    botao.addEventListener("click", function () {
        if (!showing) {
            if (botao.classList.contains("lista")) {
                botao.classList.remove("lista")
                botao.classList.add("listaclicada")
            } else {
                botao.classList.remove("listaclicada")
                botao.classList.add("lista")
            }
        }
    });
    var botaoGerador = document.getElementById("listaGerador")
    botaoGerador.addEventListener("click", function () {
        if (!showing) {
            if (botaoGerador.classList.contains("listaGerador")) {
                botaoGerador.classList.remove("listaGerador")
                botaoGerador.classList.add("geradorclicado")
            } else {
                botaoGerador.classList.remove("geradorclicado")
                botaoGerador.classList.add("listaGerador")
            }
        }
    });
    var botaoVelocidade = document.getElementById("listaVelocidade")
    botaoVelocidade.addEventListener("click", function () {
        if (!showing) {
            if (botaoVelocidade.classList.contains("listaVelocidade")) {
                botaoVelocidade.classList.remove("listaVelocidade")
                botaoVelocidade.classList.add("velocidadeclicada")
            } else {
                botaoVelocidade.classList.remove("velocidadeclicada")
                botaoVelocidade.classList.add("listaVelocidade")
            }
        }
    });

    if (!showing) {
        document.getElementById("iniciarBusca").addEventListener("click", function () {
            document.getElementById("method").value = method
            var celulasLabirinto = document.querySelectorAll("#labirinto td");
            var i = 0, j = 0
            var xs = ""
            var ys = ""
            var classes = ""
            celulasLabirinto.forEach(function (celula) {
                xs = xs + i.toString() + ","
                ys = ys + j.toString() + ","
                if (celula.classList.contains("visitada") || celula.classList.contains("path")) {
                    celula.classList.remove("visitada");
                    celula.classList.remove("path");
                    celula.classList.add("caminho");
                }
                classes = classes + celula.className + ","
                if (j < 73) {
                    j = j + 1;
                } else {
                    j = 0;
                    i = i + 1;
                }
            });
            document.getElementById("xs").value = xs
            document.getElementById("ys").value = ys
            document.getElementById("classes").value = classes
        });
    }

    var i = 0, y = 0
    var classes = classSteps.split(',')
    var grid = firstgrid.split(',')
    var index = indexChanges.split(',')
    var sleepSetTimeout_ctrl;

    function sleep(ms) {
        clearInterval(sleepSetTimeout_ctrl);
        return new Promise(resolve => sleepSetTimeout_ctrl = setTimeout(resolve, ms));
    }

    async function demo() {
        for (j = 0; j < stepnumbers - 1; j++) {
            i = 0
            switch (velo) {
                case 1:
                    await sleep(70)
                    break
                case 2:
                    await sleep(50)
                    break
                case 3:
                    await sleep(10)
                    break
                default:
                    await sleep(70)
                    break

            }
            var cells = document.querySelectorAll("#labirinto td");
            cells.forEach(function (cell) {
                if (parseInt(index[j]) == i) {
                    cell.className = classes[j]
                }
                i++
            });
        }
        await sleep(100)
        showing = false
        cells.forEach(function (cell) {
            if (cell.classList.contains("inicio")) {
                cell.setAttribute("id", "inicio")
                cell.setAttribute("draggable", "true")
                cell.setAttribute("ondragstart", "drag(event)")
                cell.setAttribute("ondrop", "")
            }
            if (cell.classList.contains("fim")) {
                cell.setAttribute("id", "fim")
                cell.setAttribute("draggable", "true")
                cell.setAttribute("ondragstart", "drag(event)")
                cell.setAttribute("ondrop", "")
            }
            if (cell.classList.contains("atual")) {
                cell.classList.remove("atual")
                cell.classList.add("caminho")
            }
        });
        document.getElementById("iniciarBusca").classList.remove("botaoiniciarespera")
        document.getElementById("iniciarBusca").classList.add("botaoiniciar")
        switch (method) {
            case 'bfs':
                document.getElementById("iniciarBusca").textContent = "Iniciar Busca em Largura!";
                document.getElementById("iniciarBusca").disabled = false
                break;
            case 'dfs':
                document.getElementById("iniciarBusca").textContent = "Iniciar Busca em Profundidade!";
                document.getElementById("iniciarBusca").disabled = false
                break;
            case 'gfs':
                document.getElementById("iniciarBusca").textContent = "Iniciar com Greedy Best-First Search!";
                document.getElementById("iniciarBusca").disabled = false
                break;
            case 'djk':
                document.getElementById("iniciarBusca").textContent = "Iniciar Busca com Dijkstra!";
                document.getElementById("iniciarBusca").disabled = false
                break;
            case 'a**':
                document.getElementById("iniciarBusca").textContent = "Iniciar Busca com o Algoritmo A*!";
                document.getElementById("iniciarBusca").disabled = false
                break;
            default:
                document.getElementById("iniciarBusca").textContent = "Escolha um Algoritmo!";
                break;
        }
    }
    if (stepnumbers > 0) {
        document.getElementById("iniciarBusca").disabled = true
        document.getElementById("iniciarBusca").textContent = "Espere a Demonstração";
        document.getElementById("iniciarBusca").classList.remove("botaoiniciar")
        document.getElementById("iniciarBusca").classList.add("botaoiniciarespera")
        showing = true
        i = 0
        var cells = document.querySelectorAll("#labirinto td");
        cells.forEach(function (cell) {
            cell.className = grid[i]
            i++
        });
        demo()
    } else {
        document.getElementById("iniciarBusca").disabled = true
        i = 0
        var cells = document.querySelectorAll("#labirinto td");
        cells.forEach(function (cell) {
            if (i == 899) {
                cell.className = "inicio"
                cell.setAttribute("id", "inicio")
                cell.setAttribute("draggable", "true")
                cell.setAttribute("ondragstart", "drag(event)")
                cell.setAttribute("ondrop", "")
            }
            if (i == 950) {
                cell.className = "fim"
                cell.setAttribute("id", "fim")
                cell.setAttribute("draggable", "true")
                cell.setAttribute("ondragstart", "drag(event)")
                cell.setAttribute("ondrop", "")
            }
            i++
        });
    }

});
