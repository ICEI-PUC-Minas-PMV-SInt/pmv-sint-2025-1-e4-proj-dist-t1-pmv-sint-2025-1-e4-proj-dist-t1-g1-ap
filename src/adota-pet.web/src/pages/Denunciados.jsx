import { useEffect, useState } from 'react'
import { Navbar } from '../components/Navbar'
import { useNavigate } from 'react-router-dom'
import { api } from '../helpers/api'

function Denunciados() {
  const [denuncias, setDenuncias] = useState([])
  const [anuncios, setAnuncios] = useState([])

  const navigate = useNavigate()

  useEffect(() => {
    const getDenuncias = async () => {
      await api.get('/Anuncios/report').then(res => {
        setDenuncias(res.data)
      })
    }

    const getAnuncios = async () => {
      await api.get('/Anuncios').then(res => {
        setAnuncios(res.data)
      })
    }

    getAnuncios()
    getDenuncias()
  }, [])

  let denunciados = []

  for (const anuncio of anuncios) {
    for (const denuncia of denuncias) {
      if (anuncio.id == denuncia.idAnuncio) {
        denunciados.push(anuncio)
      }
    }
  }

  return (
    <div className='font-inter min-h-screen bg-[#f3f3f3]'>
      <Navbar />
      <div className='p-6'>
        <h1 className='mb-6 text-center text-2xl font-semibold text-[#3b5253]'>
          Anúncios Denunciados
        </h1>

        {denunciados.length === 0 ? (
          <p className='text-center text-gray-500'>
            Nenhum anúncio denunciado.
          </p>
        ) : (
          <div className='grid gap-4'>
            {denunciados.map(anuncio => (
              <div
                key={anuncio.id}
                className='rounded-lg bg-white p-4 shadow-md transition hover:scale-[1.01]'
              >
                <h2 className='text-lg font-bold text-[#333]'>
                  {anuncio.titulo}
                </h2>
                <p className='text-sm text-gray-700'>{anuncio.descricao}</p>
                <p className='text-sm text-gray-600'>
                  Raça: {anuncio.racaAnimal}
                </p>
                <p className='text-sm text-gray-600'>
                  Idade: {anuncio.idadeAnimal}{' '}
                  {anuncio.idadeAnimal <= 1 ? 'ano' : 'anos'}
                </p>
                <button
                  onClick={() =>
                    navigate(`/anuncio/${anuncio.id}`, {
                      state: { from: 'denunciados' },
                    })
                  }
                  className='mt-2 inline-block cursor-pointer rounded-md bg-[#5e8a8c] px-3 py-1 text-sm text-white hover:scale-105'
                >
                  Ver Detalhes
                </button>
              </div>
            ))}
          </div>
        )}
      </div>
    </div>
  )
}

export { Denunciados }
