import { useRef, useState } from 'react'
import { Navbar } from '../components/Navbar'
import { api } from '../helpers/api'

function Usuario() {
  const searchInput = useRef()

  const [data, setData] = useState(null)
  const [loading, setLoading] = useState(false)
  const [warning, setWarning] = useState(null)

  const handleSearch = async event => {
    event.preventDefault()
    setWarning(null)
    setData(null)

    setLoading(true)
    await api
      .get(`/Usuarios/${searchInput.current.value}`)
      .then(res => {
        setLoading(false)
        setData(res.data)
      })
      .catch(err => {
        setLoading(false)
        console.log(err)
        setWarning(
          `Erro "${err.message}", consulte o console para mais informações`,
        )
      })
  }

  const handleEdit = async event => {
    event.preventDefault()
    setWarning(null)

    setLoading(true)
    await api
      .put(`/Usuarios/${data.id}`, {
        id: data.id,
        email: data.usuario.email,
        senha: data.usuario.senha,
        eAdmin: true,
        nome: data.nome,
        telefone: data.telefone,
        documento: data.documento,
      })
      .then(res => {
        setLoading(false)
        setWarning(`Usuário atualizado com sucesso`)
      })
      .catch(err => {
        setLoading(false)
        console.log(err)
        setWarning(
          `Erro "${err.message}", consulte o console para mais informações`,
        )
      })
  }

  const handleStatus = async event => {
    event.preventDefault()
    setWarning(null)

    let statusToSet
    let statusNumberToSet

    if (data?.usuario?.status == 0) {
      statusToSet = 'desabilitado'
      statusNumberToSet = 1
    }
    if (data?.usuario?.status == 1) {
      statusToSet = 'habilitado'
      statusNumberToSet = 0
    }

    setLoading(true)
    await api
      .post(`/Usuarios/${data.id}/${statusToSet}`)
      .then(res => {
        setLoading(false)
        setData({
          ...data,
          usuario: { ...data.usuario, status: statusNumberToSet },
        })
        return setWarning(`Usuário ${statusToSet} com sucesso`)
      })
      .catch(err => {
        setLoading(false)
        console.log(err)
        setWarning(
          `Erro "${err.message}", consulte o console para mais informações`,
        )
      })
  }

  return (
    <div className='font-inter h-screen w-screen bg-[#efefef] antialiased'>
      <Navbar />
      <div className='flex h-full w-full items-center justify-center'>
        <div className='relative w-full max-w-xs'>
          <div className='w-full overflow-hidden rounded-xl shadow-2xl shadow-neutral-300'>
            <h1 className='w-full bg-[#3b5253] py-5 text-center tracking-wider text-white'>
              Pesquisar Usuário
            </h1>

            <form
              className='flex w-full gap-2 px-2.5 pt-2.5 pb-6'
              onSubmit={event => handleSearch(event)}
            >
              <input
                type='number'
                className='flex-1 rounded-lg border-0 bg-[#ffffff] px-2 text-[#4f4f4f] outline-0 transition-all placeholder:font-bold placeholder:text-[#b1b1b1] placeholder:italic hover:scale-105 focus:scale-105'
                placeholder='ID do Usuário'
                ref={searchInput}
                min={1}
                required
              />
              <input
                type='submit'
                className='cursor-pointer rounded-lg bg-[#849e9f] p-1.5 text-xl transition-all hover:scale-105 active:scale-95'
                value={'🔍'}
              />
            </form>

            <div className='flex w-full justify-between px-4 pb-2.5'>
              <div className='flex gap-1'>
                <p className='font-bold text-[#b1b1b1] italic'>Status:</p>
                {data?.usuario?.status == 0 && (
                  <p className='font-medium text-emerald-500'>Habilitado</p>
                )}
                {data?.usuario?.status == 1 && (
                  <p className='font-medium text-red-600'>Desabilitado</p>
                )}
              </div>
              <button
                onClick={event => handleStatus(event)}
                className={`${data?.usuario?.status == 0 && 'bg-amber-700 px-2'} ${data?.usuario?.status == 1 && 'bg-emerald-700 px-5'} cursor-pointer rounded-md text-white transition-transform hover:scale-105 active:scale-95`}
              >
                {data?.usuario?.status == 0 && 'Desativar'}
                {data?.usuario?.status == 1 && 'Ativar'}
              </button>
            </div>

            <form
              className='flex w-full flex-col items-center gap-3 px-2.5 pb-2.5'
              onSubmit={event => handleEdit(event)}
            >
              <Input
                placeholder={'Nome'}
                type={'text'}
                value={data?.nome}
                onChange={event => {
                  setData({ ...data, nome: event.target.value })
                }}
              />
              <Input
                placeholder={'Email'}
                type={'email'}
                value={data?.usuario?.email}
                onChange={event => {
                  setData({
                    ...data,
                    usuario: { ...data.usuario, email: event.target.value },
                  })
                }}
              />
              <Input
                placeholder={'Senha'}
                type={'text'}
                value={data?.usuario?.senha}
                onChange={event => {
                  setData({
                    ...data,
                    usuario: { ...data.usuario, senha: event.target.value },
                  })
                }}
              />
              <Input
                placeholder={'Telefone'}
                type={'text'}
                value={data?.telefone}
                onChange={event => {
                  setData({ ...data, telefone: event.target.value })
                }}
              />
              <Input
                placeholder={'Documento'}
                type={'text'}
                value={data?.documento}
                onChange={event => {
                  setData({ ...data, documento: event.target.value })
                }}
              />
              <input
                type='submit'
                value='Editar'
                className='cursor-pointer rounded-sm bg-[#5e8a8c] px-6 py-2 text-white transition-all hover:scale-105 active:scale-95'
              />
            </form>
          </div>
          <p className='absolute -bottom-36 h-32 w-full text-center text-lg text-[#656565]'>
            {loading && 'Carregando...'}
            {warning && warning}
          </p>
        </div>
      </div>
    </div>
  )
}

function Input({ placeholder, type, value, onChange }) {
  return (
    <div className='flex w-full gap-2 rounded-lg bg-[#ffffff] px-2 py-2.5 transition-all focus-within:scale-105 hover:scale-105'>
      <span className='font-bold text-[#b1b1b1] italic'>{placeholder}:</span>
      <input
        type={type}
        value={value ? value : ''}
        onChange={onChange}
        required
        readOnly={value == null ? true : false}
        className='flex-1 text-[#4f4f4f] outline-0'
      />
    </div>
  )
}

export { Usuario }
