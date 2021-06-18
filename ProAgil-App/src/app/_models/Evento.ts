import { Lote  } from "./Lote";
import { RedeSocial } from "./RedeSocial";
import { Palestrante } from "./Palestrante";


export interface Evento {

    id: number; 
    local: string; 
    dataEvento: Date;
    tema: string; 
    imagemUrl: string; 
    qtdPessoas: number;
    telefone: string; 
    email: string; 
    lotes: Lote[]; 
    redeSociais: RedeSocial[];
    palestranteEventos: Palestrante[];

}

