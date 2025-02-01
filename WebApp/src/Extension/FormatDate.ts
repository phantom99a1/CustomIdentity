const FormatDate = (date: Date) => {
  return (
    date.toISOString().split('T')[0].replace(/-/g, '/')
  )
}

export default FormatDate