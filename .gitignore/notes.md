
## Include ile Null Reference Exception Hatasını Engelleme
- Bir Event nesnesinin Address özelliği null olduğunda, bu özelliğe erişmeye çalışmak NullReferenceException hatasına neden olur. Ancak:

 Include kullanımıyla, Address özelliği yüklendiğinden, Address boş olmayacaktır (ilişki doğru şekilde tanımlandıysa ve bir Address kaydı varsa).

- Eğer bir Event için Address kaydı yoksa bile, Include özelliği sayesinde Address null kalabilir, ancak FirstOrDefault metodu kullanıldığından, tüm işlem güvenli bir şekilde null dönebilir.

## razor pages ile butonlara baglantı eklemek

<a  asp-controller="Event" asp-action="Detail" asp-route-id="@item.Id" class="btn btn-primary">Go Details</a>

## getting event by city query

``
    public async Task<IEnumerable<Event>> GetEventByCity(string city)
    {
        return await _context.Events.Where(c=>c.Address.City.Contains(city)).ToListAsync();
    }