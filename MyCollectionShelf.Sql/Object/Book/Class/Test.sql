SELECT bs.id,
       bs.title,
       bs.series_cover,
       bs.series_summarize,

       bi.id,
       bi.title,
       bi.book_series_fk,
       bi.tome_number,
       bi.book_cover_fk,
       bi.summarize,
       bi.publish_date,
       bi.book_editor_fk,
       bi.page_number,
       bi.isbn,

       bn.id,
       bn.is_read,
       bn.start_date,
       bn.end_date,
       bn.price,
       bn.note
FROM book
         INNER JOIN book_informations bi
                    ON bi.id = book.book_information_fk
         INNER JOIN book_note bn
                    ON bn.id = book.book_note_fk
         INNER JOIN book_series bs
                    ON bi.book_series_fk = bs.id