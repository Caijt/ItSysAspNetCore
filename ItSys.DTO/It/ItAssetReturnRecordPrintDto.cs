using System;
using System.Collections.Generic;
using System.Text;

namespace ItSys.Dto
{
    public class ItAssetReturnRecordPrintDto :ItAssetReturnRecordDto
    {
        public IEnumerable<ItAssetUseRecordItemDto> assets;
    }
}
