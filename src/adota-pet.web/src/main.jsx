import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './main.css'
import { Cadastro } from './pages/Cadastro'
import { BrowserRouter, Route, Routes } from 'react-router-dom'
import { Usuario } from './pages/Usuario'
import { Anuncios } from './pages/Anuncios'
import { Criar_Anuncio } from './pages/Criar_Anuncio'
import { Editar_Anuncio } from './pages/Editar_anuncio'
import { Login } from './pages/Login'
createRoot(document.getElementById('root')).render(
  <StrictMode>
    <BrowserRouter>
      <Routes>
        <Route path='/' element={<Cadastro />} />
        <Route path='/usuario' element={<Usuario />} />
        <Route path='/anuncios' element={<Anuncios />} />
        <Route path='/criar_anuncio' element={<Criar_Anuncio />} />
        <Route path='/editar_anuncio/:id' element={<Editar_Anuncio />} />
        <Route path='/login' element={<Login />} />
      </Routes>
    </BrowserRouter>
  </StrictMode>,
)
