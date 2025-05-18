import { useEffect, useState } from 'react'
import { Navbar } from '../components/Navbar'
import { api } from '../helpers/api'
import { useNavigate } from 'react-router-dom'

function Anuncios() {
  const [anuncios, setAnuncios] = useState([])
  const [warning, setWarning] = useState('')
  const [liked, setLiked] = useState([])

  useEffect(() => {
    const getLiked = async () => {
      await api
        .get('/Anuncios/like')
        .then(res => setLiked(res.data))
        .catch(err => {
          console.log(err)
          setWarning(
            `Erro "${err.message}", consulte o console para mais informações`,
          )
        })
    }
    const getAnuncios = async () => {
      await api
        .get('/Anuncios')
        .then(res => setAnuncios(res.data))
        .catch(err => {
          console.log(err)
          setWarning(
            `Erro "${err.message}", consulte o console para mais informações`,
          )
        })
    }

    getLiked()
    getAnuncios()
  }, [])

  function AnuncioCard({ anuncio }) {
    const [isLiked, setIsLiked] = useState(false)
    const [isReported, setIsReported] = useState(false)

    const navigate = useNavigate()

    useEffect(() => {
      for (const like of liked) {
        if (anuncio.id == like.idAnuncio) {
          return setIsLiked(true)
        }
      }
    }, [])

    const handleLike = async () => {
      await api.post(`/Anuncios/like/${anuncio.id}`).then(res => {
        setIsLiked(true)
      })
    }

    const handleDislike = async () => {
      await api.delete(`/Anuncios/like/${anuncio.id}`).then(res => {
        setIsLiked(false)
      })
    }

    const handleReport = async () => {
      setIsReported(true)
      await api.post(`/Anuncios/report/${anuncio.id}`)
    }

    return (
      <div className='rounded-xl bg-white p-6 shadow-md transition-all hover:scale-[1.01]'>
        <div className='grid grid-cols-2 gap-4 text-sm font-medium text-[#333] sm:grid-cols-3 lg:grid-cols-6'>
          <div>
            <span className='block text-xs text-gray-500'>Título</span>
            {anuncio.titulo}
          </div>
          <div>
            <span className='block text-xs text-gray-500'>Descrição</span>
            {anuncio.descricao}
          </div>
          <div>
            <span className='block text-xs text-gray-500'>Raça</span>
            {anuncio.racaAnimal}
          </div>
          <div>
            <span className='block text-xs text-gray-500'>Idade</span>
            {Number(anuncio.idadeAnimal)}{' '}
            {Number(anuncio.idadeAnimal) <= 1 ? 'ano' : 'anos'}
          </div>
          <div className='mt-4 flex flex-wrap items-center gap-2 lg:mt-0'>
            <button
              onClick={() => navigate(`/anuncio/${anuncio.id}`)}
              className='cursor-pointer rounded-md bg-[#5e8a8c] px-4 py-2 text-white transition-all hover:scale-105 active:scale-95'
            >
              Detalhes
            </button>

            <button
              onClick={() => navigate(`/editar_anuncio/${anuncio.id}`)}
              className='cursor-pointer rounded-md bg-yellow-500 px-4 py-2 text-white transition-all hover:scale-105 active:scale-95'
            >
              Editar
            </button>

            <button
              onClick={() => {
                if (!isLiked) {
                  return handleLike()
                }
                return handleDislike()
              }}
              className='cursor-pointer rounded-md bg-pink-400 px-4 py-2 text-white transition-all hover:scale-105 active:scale-95'
            >
              {isLiked ? 'Descurtir' : 'Curtir'}
            </button>
            {!isReported && (
              <button
                onClick={() => handleReport()}
                className='cursor-pointer rounded-md bg-red-400 px-4 py-2 text-white transition-all hover:scale-105 active:scale-95'
              >
                Denunciar
              </button>
            )}
            {isReported && <p>Anúncio denunciado</p>}
          </div>
        </div>
      </div>
    )
  }

  return (
    <div className='font-inter min-h-screen w-screen bg-[#efefef] antialiased'>
      <Navbar />
      <div className='p-6'>
        <h1 className='mb-8 text-center text-2xl font-semibold text-[#3b5253]'>
          Anúncios
        </h1>
        <div className='flex flex-col gap-6'>
          {warning && <p>{warning}</p>}
          {anuncios &&
            anuncios.map(anuncio => (
              <AnuncioCard key={anuncio.id} anuncio={anuncio} />
            ))}
        </div>
      </div>
    </div>
  )
}

export { Anuncios }
