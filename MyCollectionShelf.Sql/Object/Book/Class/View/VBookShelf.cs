using System.ComponentModel;
using System.Runtime.CompilerServices;
using MyCollectionShelf.Sql.Object.Interface;
using SQLite;

namespace MyCollectionShelf.Sql.Object.Book.Class.View;

public class VBookShelf : ISql, INotifyPropertyChanged
{
    public event PropertyChangedEventHandler? PropertyChanged;

    protected virtual void OnPropertyChanged([CallerMemberName] string? propertyName = null)
        => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

    private long _bookSeriesId;

    [Column("book_series_id")]
    public long BookSeriesId
    {
        get => _bookSeriesId;
        set
        {
            _bookSeriesId = value;
            OnPropertyChanged();
        }
    }
    
    private string _bookSeriesTitle = string.Empty;
    
    [Column("title")]
    public string BookSeriesTitle
    {
        get => _bookSeriesTitle;
        set
        {
            _bookSeriesTitle = value;
            OnPropertyChanged();
        }
    }
    
    private string _bookSeriesCover = string.Empty;
    
    [Column("series_cover")]
    public string BookSeriesCover
    {
        get => _bookSeriesCover;
        set
        {
            _bookSeriesCover = value;
            OnPropertyChanged();
        }
    }
    
    private string _seriesSummarize = string.Empty;
    
    [Column("series_summarize")]
    public string SeriesSummarize
    {
        get => _seriesSummarize;
        set
        {
            _seriesSummarize = value;
            OnPropertyChanged();
        }
    }
    
    private long _averagePage;

    [Column("average_page")]
    public long AveragePage
    {
        get => _averagePage;
        set
        {
            _averagePage = value;
            OnPropertyChanged();
        }
    }
    
    private long _bookEditorListId;

    [Column("book_editor_list_id")]
    public long BookEditorListId
    {
        get => _bookEditorListId;
        set
        {
            _bookEditorListId = value;
            OnPropertyChanged();
        }
    }
    
    private string _editor = string.Empty;
    
    [Column("editor")]
    public string Editor
    {
        get => _editor;
        set
        {
            _editor = value;
            OnPropertyChanged();
        }
    }
    
    private long _notRead;

    [Column("not_read")]
    public long NotRead
    {
        get => _notRead;
        set
        {
            _notRead = value;
            OnPropertyChanged();
        }
    }
    
    private long _read;

    [Column("read")]
    public long Read
    {
        get => _read;
        set
        {
            _read = value;
            OnPropertyChanged();
        }
    }
    
    public string Definition =>
        """
        CREATE VIEW IF NOT EXISTS v_book_shelf AS
        SELECT bs.id                                            AS book_series_id,
               bs.title,
               bs.series_cover,
               bs.series_summarize,
               AVG(bi.page_number)                              as average_page,
               bel.id                                           AS book_editor_list_id,
               bel.editor,
               SUM(CASE WHEN bn.is_read == 0 THEN 1 ELSE 0 END) AS not_read,
               SUM(CASE WHEN bn.is_read == 1 THEN 1 ELSE 0 END) AS read

        FROM book
                 INNER JOIN book_informations bi
                            ON bi.id = book.book_information_fk
                 INNER JOIN book_note bn
                            ON bn.id = book.book_note_fk
                 INNER JOIN book_series bs
                            ON bi.book_series_fk = bs.id
                 INNER JOIN book_editor_list bel
                            ON bi.book_editor_fk = bel.id
        GROUP BY bs.id
        """;
}